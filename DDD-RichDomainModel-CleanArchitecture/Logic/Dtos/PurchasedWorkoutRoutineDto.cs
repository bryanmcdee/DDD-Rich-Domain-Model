using System;

namespace Logic.Dtos
{
    public class PurchasedWorkoutRoutineDto
    {
        public WorkoutRoutineDto WorkoutRoutine { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
