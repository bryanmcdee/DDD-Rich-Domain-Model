using Logic.ValueObjects;
using System;
using System.Collections.Generic;

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

        public virtual AthleteStatusType Status { get; set; }
        public virtual DateTime? StatusExpirationDate { get; set; }

        private decimal _moneySpent;
        public virtual Dollars MoneySpent
        {
            get => Dollars.Of(_moneySpent);
            set => _moneySpent = value;
        }

        public virtual IList<PurchasedWorkoutRoutine> PurchasedWorkoutRoutine { get; set; }
    }
}
