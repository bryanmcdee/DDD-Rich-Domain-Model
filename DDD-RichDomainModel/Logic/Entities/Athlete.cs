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
            get => AthleteName.Create(_name).Value;
            set => _name = value.Value;
        }

        private string _email;

        public virtual Email Email
        {
            get => Email.Create(_email).Value;
            set => _email = value.Value;
        }

        public virtual AthleteStatusType Status { get; set; }
        public virtual DateTime? StatusExpirationDate { get; set; }
        public virtual decimal MoneySpent { get; set; }
        public virtual IList<PurchasedWorkoutRoutine> PurchasedWorkoutRoutine { get; set; }
    }
}
