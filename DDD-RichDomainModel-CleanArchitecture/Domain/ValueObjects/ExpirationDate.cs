using System;

namespace Domain.ValueObjects
{
    public class ExpirationDate : ValueObject<ExpirationDate>
    {
        public static readonly ExpirationDate Infinate = new ExpirationDate(null);
        public DateTime? Date { get; }
        public bool IsExpired => this != Infinate && Date < DateTime.UtcNow;

        private ExpirationDate(DateTime? date)
        {
            Date = date;
        }

        public static Result<ExpirationDate> Create(DateTime date)
        {
            return Result.Ok(new ExpirationDate(date));
        }

        protected override bool EqualsCore(ExpirationDate other)
        {
            return Date == other.Date;
        }

        protected override int GetHashCodeCore()
        {
            return Date.GetHashCode();
        }

        public static implicit operator DateTime? (ExpirationDate date)
        {
            return date.Date;
        }

        public static explicit operator ExpirationDate(DateTime? date)
        {
            if (date.HasValue)
                return Create(date.Value).Value;

            return Infinate;
        }
    }
}
