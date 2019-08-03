using Logic.ValueObjects;
using System;
using System.Collections.Generic;

namespace Logic.Entities
{
    public class Athlete : Entity
    {
        public virtual AthleteName Name { get; set; }
        public virtual Email Email { get; set; }
        public virtual AthleteStatusType Status { get; set; }
        public virtual DateTime? StatusExpirationDate { get; set; }
        public virtual decimal MoneySpent { get; set; }
        public virtual IList<PurchasedWorkoutRoutine> PurchasedWorkoutRoutine { get; set; }
    }
}
