using Domain.ValueObjects;
using System;

namespace Domain.Entities
{
    public class PurchasedWorkoutRoutine : Entity
    {
        public virtual WorkoutRoutine WorkoutRoutine { get; protected set; }
        public virtual Athlete Athlete { get; protected set; }
        public virtual DateTime PurchaseDate { get; protected set; }

        private decimal _price;
        public virtual Dollars Price
        {
            get => Dollars.Of(_price);
            protected set => _price = value;
        }

        private DateTime? _expirationDate;
        public virtual ExpirationDate ExpirationDate
        {
            get => (ExpirationDate)_expirationDate;
            protected set => _expirationDate = value;
        }

        protected PurchasedWorkoutRoutine() { }
        internal PurchasedWorkoutRoutine(WorkoutRoutine workoutRoutine, Athlete athlete, Dollars price, ExpirationDate expirationDate)
        {
            if (price == null || price <= 0)
                throw new ArgumentException(nameof(price));

            if (expirationDate == null || expirationDate.IsExpired)
                throw new ArgumentException(nameof(expirationDate));

            WorkoutRoutine = workoutRoutine ?? throw new ArgumentNullException(nameof(workoutRoutine));
            Athlete = athlete ?? throw new ArgumentNullException(nameof(athlete));
            Price = price;
            ExpirationDate = expirationDate;
            PurchaseDate = DateTime.UtcNow;
        }
    }
}
