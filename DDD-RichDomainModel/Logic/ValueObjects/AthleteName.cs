using System;

namespace Logic.ValueObjects
{
    public class AthleteName : ValueObject<AthleteName>
    {
        public string Value { get; }

        public AthleteName(string value)
        {
            Value = value;
        }
        protected override bool EqualsCore(AthleteName other)
        {
            return Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}
