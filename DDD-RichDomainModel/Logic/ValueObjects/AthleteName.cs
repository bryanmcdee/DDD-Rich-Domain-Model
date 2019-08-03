using System;

namespace Logic.ValueObjects
{
    public class AthleteName : ValueObject<AthleteName>
    {
        public string Value { get; }

        private AthleteName(string value)
        {
            Value = value;
        }

        public static Result<AthleteName> Create(string athleteName)
        {
            athleteName = (athleteName ?? string.Empty).Trim();

            if (athleteName.Length == 0)
                return Result.Fail<AthleteName>("Athlete name should not be empty.");

            if (athleteName.Length > 100)
                return Result.Fail<AthleteName>("Athlete name can only be 100 characters long.");

            return Result.Ok(new AthleteName(athleteName));
        }

        protected override bool EqualsCore(AthleteName other)
        {
            return Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static implicit operator string (AthleteName athleteName)
        {
            return athleteName.Value;
        }

        public static explicit operator AthleteName(string athleteName)
        {
            return Create(athleteName).Value;
        }
    }
}
