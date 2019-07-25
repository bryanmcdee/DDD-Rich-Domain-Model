using FluentNHibernate.Mapping;
using Logic.Entities;

namespace Logic.Mappings
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
