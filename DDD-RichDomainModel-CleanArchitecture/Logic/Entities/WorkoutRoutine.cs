using Newtonsoft.Json;

namespace Logic.Entities
{
    public class WorkoutRoutine : Entity
    {
        public virtual string Name { get; set; }

        [JsonIgnore]
        public virtual LicensingModelType LicensingModel { get; set; }
    }
}
