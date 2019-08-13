using FluentNHibernate.Mapping;
using Domain.Entities;
using System;

namespace Domain.Mappings
{
    public class PurchasedWorkoutRoutineMap : ClassMap<PurchasedWorkoutRoutine>
    {
        public PurchasedWorkoutRoutineMap()
        {
            Id(x => x.Id);

            Map(x => x.Price).CustomType<decimal?>().Access.CamelCaseField(Prefix.Underscore);
            Map(x => x.PurchaseDate);
            Map(x => x.ExpirationDate).CustomType<DateTime?>().Access.CamelCaseField(Prefix.Underscore).Nullable();
            Map(x => x.WorkoutRoutine);
            Map(x => x.Athlete);

            References(x => x.WorkoutRoutine).LazyLoad(Laziness.False).ReadOnly();
        }
    }
}
