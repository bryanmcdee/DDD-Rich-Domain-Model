using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Logic.Entities
{
    public class Athlete : Entity
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name is too long")]
        public virtual string Name { get; set; }

        [Required]
        [RegularExpression(@"^(.+)@(.+)$", ErrorMessage = "Email is invalid")]
        public virtual string Email { get; set; }

        public virtual AthleteStatusType Status { get; set; }
        public virtual DateTime? StatusExpirationDate { get; set; }
        public virtual decimal MoneySpent { get; set; }
        public virtual IList<PurchasedWorkoutRoutine> PurchasedWorkoutRoutine { get; set; }
    }
}
