using FluentNHibernate.Mapping;
using Logic.Entities;

namespace Logic.Mappings
{
    public class AthleteMap : ClassMap<Athlete>
    {
        public AthleteMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.Email);
            Map(x => x.Status).CustomType<int>();
            Map(x => x.StatusExpirationDate).Nullable();
            Map(x => x.MoneySpent);

            HasMany(x => x.PurchasedWorkoutRoutine);
        }
    }
}
