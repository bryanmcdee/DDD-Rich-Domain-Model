using Logic.ValueObjects;
using System;

namespace Logic.Entities
{
    public class PurchasedWorkoutRoutine : Entity
    {
        public virtual long WorkoutRoutineId { get; set; }
        public virtual WorkoutRoutine WorkoutRoutine { get; set; }
        public virtual long AthleteId { get; set; }
        public virtual DateTime PurchaseDate { get; set; }

        private decimal _price;
        public virtual Dollars Price
        {
            get => Dollars.Of(_price);
            set => _price = value;
        }

        private DateTime? _expirationDate;
        public virtual ExpirationDate ExpirationDate
        {
            get => (ExpirationDate)_expirationDate;
            set => _expirationDate = value;
        }
    }
}
