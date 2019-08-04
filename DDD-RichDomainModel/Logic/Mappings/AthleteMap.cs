using FluentNHibernate.Mapping;
using Logic.Entities;
using System;

namespace Logic.Mappings
{
    public class AthleteMap : ClassMap<Athlete>
    {
        public AthleteMap()
        {
            Id(x => x.Id);

            Map(x => x.Name).CustomType<string>().Access.CamelCaseField(Prefix.Underscore);
            Map(x => x.Email).CustomType<string>().Access.CamelCaseField(Prefix.Underscore);
            Map(x => x.Status).CustomType<int>();
            Map(x => x.StatusExpirationDate).CustomType<DateTime?>().Access.CamelCaseField(Prefix.Underscore).Nullable();
            Map(x => x.MoneySpent).CustomType<decimal>().Access.CamelCaseField(Prefix.Underscore);

            HasMany(x => x.PurchasedWorkoutRoutine).Access.CamelCaseField(Prefix.Underscore);
        }
    }
}
