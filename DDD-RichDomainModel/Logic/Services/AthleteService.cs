using System;
using System.Linq;
using Logic.Entities;

namespace Logic.Services
{
    public class AthleteService
    {
        private readonly WorkoutRoutineService workoutRoutineService;

        public AthleteService(WorkoutRoutineService workoutRoutineSvc)
        {
            workoutRoutineService = workoutRoutineSvc;
        }

        private decimal CalculatePrice(AthleteStatusType athleteStatusType, DateTime? statusExpirationDate, LicensingModelType licensingModelType)
        {
            decimal price;
            switch (licensingModelType)
            {
                case LicensingModelType.TwoDays:
                    price = 2;
                    break;
                case LicensingModelType.ThirtyDays:
                    price = 4;
                    break;
                case LicensingModelType.LifeLong:
                    price = 8;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (athleteStatusType == AthleteStatusType.Advanced && (statusExpirationDate == null || statusExpirationDate.Value >= DateTime.UtcNow))
            {
                price = price * 0.75m;
            }

            return price;
        }

        public void PurchaseWorkoutRoutine(Athlete athlete, WorkoutRoutine workoutRoutine)
        {
            DateTime? expirationDate = workoutRoutineService.GetExpirationDate(workoutRoutine.LicensingModel);
            decimal price = CalculatePrice(athlete.Status, athlete.StatusExpirationDate, workoutRoutine.LicensingModel);

            var purchasedWorkoutRoutine = new PurchasedWorkoutRoutine
            {
                WorkoutRoutineId = workoutRoutine.Id,
                AthleteId = athlete.Id,
                ExpirationDate = expirationDate,
                Price = price
            };

            athlete.PurchasedWorkoutRoutine.Add(purchasedWorkoutRoutine);
            athlete.MoneySpent += price;
        }

        public bool UpgradeAthleteStatus(Athlete athlete)
        {
            // at least 2 active workout routines during the last 30 days
            if (athlete.PurchasedWorkoutRoutine.Count(x => x.ExpirationDate == null || x.ExpirationDate.Value >= DateTime.UtcNow.AddDays(-30)) < 2)
                return false;

            // at least 100 dollars spent during the last year
            if (athlete.PurchasedWorkoutRoutine.Where(x => x.PurchaseDate > DateTime.UtcNow.AddYears(-1)).Sum(x => x.Price) < 100m)
                return false;

            athlete.Status = AthleteStatusType.Advanced;
            athlete.StatusExpirationDate = DateTime.UtcNow.AddYears(1);

            return true;
        }
    }
}
