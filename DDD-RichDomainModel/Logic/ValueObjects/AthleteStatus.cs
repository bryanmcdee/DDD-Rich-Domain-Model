using Logic.Entities;
using System;

namespace Logic.ValueObjects
{
    public class AthleteStatus : ValueObject<AthleteStatus>
    {
        public static readonly AthleteStatus Regular = new AthleteStatus(AthleteStatusType.Regular, ExpirationDate.Infinate);
        public AthleteStatusType Type { get; }

        private readonly DateTime? _expirationDate;
        public ExpirationDate ExpirationDate => (ExpirationDate)_expirationDate;

        public bool IsAdvanced => Type == AthleteStatusType.Advanced && !ExpirationDate.IsExpired;

        private AthleteStatus() { }

        private AthleteStatus(AthleteStatusType type, ExpirationDate expirationDate)
        {
            Type = type;
            _expirationDate = expirationDate ?? throw new ArgumentNullException(nameof(expirationDate));
        }

        public AthleteStatus UpgradeToAdvanced()
        {
            return new AthleteStatus(AthleteStatusType.Advanced, ExpirationDate.Create(DateTime.UtcNow.AddYears(1)).Value);
        }

        protected override bool EqualsCore(AthleteStatus other)
        {
            return Type == other.Type && ExpirationDate == other.ExpirationDate;
        }

        protected override int GetHashCodeCore()
        {
            return Type.GetHashCode() ^ ExpirationDate.GetHashCode();
        }        
    }
}
