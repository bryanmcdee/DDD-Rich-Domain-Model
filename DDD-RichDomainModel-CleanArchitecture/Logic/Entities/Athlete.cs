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
            protected set => _email = value;
        }

        public virtual AthleteStatus Status { get; set; }

        private decimal _moneySpent;
        public virtual Dollars MoneySpent
        {
            get => Dollars.Of(_moneySpent);
            protected set => _moneySpent = value;
        }

        private readonly IList<PurchasedWorkoutRoutine> _purchasedWorkoutRoutine;
        public virtual IReadOnlyList<PurchasedWorkoutRoutine> PurchasedWorkoutRoutine => _purchasedWorkoutRoutine.ToList();

        protected Athlete()
        {
            _purchasedWorkoutRoutine = new List<PurchasedWorkoutRoutine>();
        }

        public Athlete(AthleteName athleteName, Email email) : this()
        {
            _name = athleteName ?? throw new ArgumentNullException(nameof(athleteName));
            _email = email ?? throw new ArgumentNullException(nameof(email));

            MoneySpent = Dollars.Of(0);
            Status = AthleteStatus.Regular;
        }

        public virtual void PurchaseWorkoutRoutine(WorkoutRoutine workoutRoutine)
        {
            ExpirationDate expirationDate = workoutRoutine.GetExpirationDate();
            Dollars price = workoutRoutine.CalculatePrice(Status);

            var purchasedWorkoutRoutine = new PurchasedWorkoutRoutine(workoutRoutine, this, price, expirationDate); 
            _purchasedWorkoutRoutine.Add(purchasedWorkoutRoutine);
            MoneySpent += price;
        }

        public virtual bool UpgradeStatusToAdvanced()
        {
            // at least 2 active workout routines during the last 30 days
            if (PurchasedWorkoutRoutine.Count(x => x.ExpirationDate == ExpirationDate.Infinate || x.ExpirationDate.Date >= DateTime.UtcNow.AddDays(-30)) < 2)
                return false;

            // at least 100 dollars spent during the last year
            if (PurchasedWorkoutRoutine.Where(x => x.PurchaseDate > DateTime.UtcNow.AddYears(-1)).Sum(x => x.Price) < 100m)
                return false;

            Status = Status.UpgradeToAdvanced();

            return true;
        }
    }
}
