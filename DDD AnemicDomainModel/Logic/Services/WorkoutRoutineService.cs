using System;
using Logic.Entities;

namespace Logic.Services
{
    public class WorkoutRoutineService
    {
        public DateTime? GetExpirationDate(LicensingModelType licensingModelType)
        {
            DateTime? result;

            switch (licensingModelType)
            {
                case LicensingModelType.TwoDays:
                    result = DateTime.UtcNow.AddDays(2);
                    break;

                case LicensingModelType.LifeLong:
                    result = null;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }
    }
}
