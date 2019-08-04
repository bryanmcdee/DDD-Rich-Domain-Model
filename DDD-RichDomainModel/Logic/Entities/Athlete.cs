using Logic.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Entities
{
    public class Athlete : Entity
    {      
        private string _name;
        public virtual AthleteName Name
        {
            get => (AthleteName)_name;
            set => _name = value;
        }

        private string _email;

        public virtual Email Email
        {
            get => (Email)_email;
            set => _email = value;
        }

        private DateTime? _statusExpirationDate;
        public virtual ExpirationDate StatusExpirationDate
        {
            get => (ExpirationDate)_statusExpirationDate;
            set => _statusExpirationDate = value;
        }

        private decimal _moneySpent;
        public virtual Dollars MoneySpent
        {
            get => Dollars.Of(_moneySpent);
            protected set => _moneySpent = value;
        }

        private readonly IList<PurchasedWorkoutRoutine> _purchasedWorkoutRoutine;
        public virtual IReadOnlyList<PurchasedWorkoutRoutine> PurchasedWorkoutRoutine => _purchasedWorkoutRoutine.ToList();

        public virtual AthleteStatusType Status { get; set; }

        protected Athlete()
        {
            _purchasedWorkoutRoutine = new List<PurchasedWorkoutRoutine>();
        }

        public Athlete(AthleteName athleteName, Email email) : this()
        {
            _name = athleteName ?? throw new ArgumentNullException(nameof(athleteName));
            _email = email ?? throw new ArgumentNullException(nameof(email));

            MoneySpent = Dollars.Of(0);
            Status = AthleteStatusType.Regular;
            StatusExpirationDate = null;
        }

        public virtual void AddPurchasedMovie(WorkoutRoutine workoutRoutine, ExpirationDate expirationDate, Dollars price)
        {
            var purchasedWorkoutRoutine = new PurchasedWorkoutRoutine
            {
                WorkoutRoutineId = workoutRoutine.Id,
                AthleteId = Id,
                ExpirationDate = expirationDate,
                Price = price,
                PurchaseDate = DateTime.UtcNow
            };

            _purchasedWorkoutRoutine.Add(purchasedWorkoutRoutine);
            MoneySpent += price;
        }
    }
}
