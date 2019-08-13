using FluentNHibernate.Mapping;
using Domain.Entities;
using System;

namespace Domain.Mappings
{
    public class AthleteMap : ClassMap<Athlete>
    {
        public AthleteMap()
        {
            Id(x => x.Id);

            Map(x => x.Name).CustomType<string>().Access.CamelCaseField(Prefix.Underscore);
            Map(x => x.Email).CustomType<string>().Access.CamelCaseField(Prefix.Underscore);
            Map(x => x.MoneySpent).CustomType<decimal>().Access.CamelCaseField(Prefix.Underscore);

            Component(x => x.Status, y =>
            {
                y.Map(x => x.Type, "Status").CustomType<int>();
                y.Map(x => x.ExpirationDate, "StatusExpirationDate").CustomType<DateTime?>()
                    .Access.CamelCaseField(Prefix.Underscore)
                    .Nullable();
            });

            HasMany(x => x.PurchasedWorkoutRoutine).Access.CamelCaseField(Prefix.Underscore);
        }
    }
}
