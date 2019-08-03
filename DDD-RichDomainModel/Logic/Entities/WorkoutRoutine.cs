namespace Logic.Entities
{
    public class WorkoutRoutine : Entity
    {
        public virtual string Name { get; set; }
        public virtual LicensingModelType LicensingModel { get; set; }
    }
}
