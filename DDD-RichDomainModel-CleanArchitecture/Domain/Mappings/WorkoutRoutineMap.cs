using FluentNHibernate.Mapping;
using Domain.Entities;

namespace Domain.Mappings
{
    public class WorkoutRoutineMap : ClassMap<WorkoutRoutine>
    {
        public WorkoutRoutineMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.LicensingModel).CustomType<int>();
        }
    }
}
