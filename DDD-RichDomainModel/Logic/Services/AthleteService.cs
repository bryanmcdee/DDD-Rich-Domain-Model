using System;
using System.Linq;
using Logic.Entities;
using Logic.ValueObjects;

namespace Logic.Services
{
    public class AthleteService
    {
        private readonly WorkoutRoutineService workoutRoutineService;

        public AthleteService(WorkoutRoutineService workoutRoutineSvc)
        {
            workoutRoutineService = workoutRoutineSvc;
        }

        private Dollars CalculatePrice(AthleteStatusType athleteStatusType, ExpirationDate statusExpirationDate, LicensingModelType licensingModelType)
        {
            Dollars price;
            switch (licensingModelType)
            {
                case LicensingModelType.TwoDays:
                    price = Dollars.Of(2);
                    break;
                case LicensingModelType.ThirtyDays:
                    price = Dollars.Of(4);
                    break;
                case LicensingModelType.LifeLong:
                    price = Dollars.Of(8);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (athleteStatusType == AthleteStatusType.Advanced && !statusExpirationDate.IsExpired)
            {
                price = price * 0.75m;
            }

            return price;
        }

        public void PurchaseWorkoutRoutine(Athlete athlete, WorkoutRoutine workoutRoutine)
        {
            ExpirationDate expirationDate = workoutRoutineService.GetExpirationDate(workoutRoutine.LicensingModel);
            Dollars price = CalculatePrice(athlete.Status, athlete.StatusExpirationDate, workoutRoutine.LicensingModel);

            var purchasedWorkoutRoutine = new PurchasedWorkoutRoutine
            {
                WorkoutRoutineId = workoutRoutine.Id,
                AthleteId = athlete.Id,
                ExpirationDate = expirationDate,
                Price = price,
                PurchaseDate = DateTime.UtcNow
            };

            athlete.PurchasedWorkoutRoutine.Add(purchasedWorkoutRoutine);
            athlete.MoneySpent += price;
        }

        public bool UpgradeAthleteStatus(Athlete athlete)
        {
            // at least 2 active workout routines during the last 30 days
            if (athlete.PurchasedWorkoutRoutine.Count(x => x.ExpirationDate == ExpirationDate.Infinate || x.ExpirationDate.Date >= DateTime.UtcNow.AddDays(-30)) < 2)
                return false;

            // at least 100 dollars spent during the last year
            if (athlete.PurchasedWorkoutRoutine.Where(x => x.PurchaseDate > DateTime.UtcNow.AddYears(-1)).Sum(x => x.Price) < 100m)
                return false;

            athlete.Status = AthleteStatusType.Advanced;
            athlete.StatusExpirationDate = (ExpirationDate)DateTime.UtcNow.AddYears(1);

            return true;
        }
    }
}
