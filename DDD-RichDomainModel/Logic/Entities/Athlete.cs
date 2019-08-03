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
            set => _moneySpent = value;
        }

        public virtual AthleteStatusType Status { get; set; }
        public virtual IList<PurchasedWorkoutRoutine> PurchasedWorkoutRoutine { get; set; }
    }
}
