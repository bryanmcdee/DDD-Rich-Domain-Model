using Domain.ValueObjects;
using System;

namespace Domain.Entities
{
    public class WorkoutRoutine : Entity
    {
        public virtual string Name { get; protected set; }
        public virtual LicensingModelType LicensingModel { get; protected set; }

        public ExpirationDate GetExpirationDate()
        {        
            switch (LicensingModel)
            {
                case LicensingModelType.TwoDays:
                    return (ExpirationDate)DateTime.UtcNow.AddDays(2);
                case LicensingModelType.LifeLong:
                    return ExpirationDate.Infinate;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public virtual Dollars CalculatePrice(AthleteStatus athleteStatus)
        {
            decimal discountModifyer = 1 - athleteStatus.GetDiscount();
            switch (LicensingModel)
            {
                case LicensingModelType.TwoDays:
                    return Dollars.Of(2) * discountModifyer;
                case LicensingModelType.ThirtyDays:
                    return Dollars.Of(4) * discountModifyer;
                case LicensingModelType.LifeLong:
                    return Dollars.Of(8) * discountModifyer;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
