using System;
using Logic.Entities;
using Logic.ValueObjects;

namespace Logic.Services
{
    public class WorkoutRoutineService
    {
        public ExpirationDate GetExpirationDate(LicensingModelType licensingModelType)
        {
            ExpirationDate result;

            switch (licensingModelType)
            {
                case LicensingModelType.TwoDays:
                    result = (ExpirationDate)DateTime.UtcNow.AddDays(2);
                    break;

                case LicensingModelType.LifeLong:
                    result = ExpirationDate.Infinate;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }
    }
}
