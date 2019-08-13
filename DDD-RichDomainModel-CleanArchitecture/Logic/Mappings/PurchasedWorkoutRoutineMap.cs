using FluentNHibernate.Mapping;
using Logic.Entities;

namespace Logic.Mappings
{
    public class PurchasedWorkoutRoutineMap : ClassMap<PurchasedWorkoutRoutine>
    {
        public PurchasedWorkoutRoutineMap()
        {
            Id(x => x.Id);

            Map(x => x.Price);
            Map(x => x.PurchaseDate);
            Map(x => x.ExpirationDate).Nullable();
            Map(x => x.WorkoutRoutineId);
            Map(x => x.AthleteId);

            References(x => x.WorkoutRoutine).LazyLoad(Laziness.False).ReadOnly();
        }
    }
}
