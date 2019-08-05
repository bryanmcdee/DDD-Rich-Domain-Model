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

        private Dollars CalculatePrice(AthleteStatus athleteStatus, LicensingModelType licensingModelType)
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

            if (athleteStatus.IsAdvanced)
            {
                price = price * 0.75m;
            }

            return price;
        }

        public void PurchaseWorkoutRoutine(Athlete athlete, WorkoutRoutine workoutRoutine)
        {
            ExpirationDate expirationDate = workoutRoutineService.GetExpirationDate(workoutRoutine.LicensingModel);
            Dollars price = CalculatePrice(athlete.Status, workoutRoutine.LicensingModel);
            
            athlete.AddPurchasedMovie(workoutRoutine, expirationDate, price);        
        }

        public bool UpgradeAthleteStatus(Athlete athlete)
        {
            // at least 2 active workout routines during the last 30 days
            if (athlete.PurchasedWorkoutRoutine.Count(x => x.ExpirationDate == ExpirationDate.Infinate || x.ExpirationDate.Date >= DateTime.UtcNow.AddDays(-30)) < 2)
                return false;

            // at least 100 dollars spent during the last year
            if (athlete.PurchasedWorkoutRoutine.Where(x => x.PurchaseDate > DateTime.UtcNow.AddYears(-1)).Sum(x => x.Price) < 100m)
                return false;

            athlete.Status = athlete.Status.UpgradeToAdvanced();

            return true;
        }
    }
}
