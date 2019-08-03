using FluentNHibernate.Mapping;
using Logic.Entities;
using System;

namespace Logic.Mappings
{
    public class PurchasedWorkoutRoutineMap : ClassMap<PurchasedWorkoutRoutine>
    {
        public PurchasedWorkoutRoutineMap()
        {
            Id(x => x.Id);

            Map(x => x.Price).CustomType<decimal?>().Access.CamelCaseField(Prefix.Underscore);
            Map(x => x.PurchaseDate);
            Map(x => x.ExpirationDate).CustomType<DateTime?>().Access.CamelCaseField(Prefix.Underscore).Nullable();
            Map(x => x.WorkoutRoutineId);
            Map(x => x.AthleteId);

            References(x => x.WorkoutRoutine).LazyLoad(Laziness.False).ReadOnly();
        }
    }
}
