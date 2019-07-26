using System;
using Newtonsoft.Json;

namespace Logic.Entities
{
    public class PurchasedWorkoutRoutine : Entity
    {
        [JsonIgnore]
        public virtual long WorkoutRoutineId { get; set; }

        public virtual WorkoutRoutine WorkoutRoutine { get; set; }

        [JsonIgnore]
        public virtual long AthleteId { get; set; }

        public virtual decimal Price { get; set; }

        public virtual DateTime PurchaseDate { get; set; }

        public virtual DateTime? ExpirationDate { get; set; }
    }
}
