﻿namespace Logic.ValueObjects
{
    public class Dollars : ValueObject<Dollars>
    {
        private const decimal MaxDollarAmount = 1_000_000;

        public decimal Value { get; }

        private Dollars(decimal value)
        {
            Value = value;
        }

        public static Result<Dollars> Create(decimal dollarAmount)
        {
            if (dollarAmount < 0)
                return Result.Fail<Dollars>("Dollar amount can not be negetive.");

            if (dollarAmount > MaxDollarAmount)
                return Result.Fail<Dollars>($"Dollar amount can not be greater than {MaxDollarAmount}.");

            if (dollarAmount % 0.01m > 0)
                return Result.Fail<Dollars>("Dollar amount can not contain part of a penny.");

            return Result.Ok(new Dollars(dollarAmount));
        }

        protected override bool EqualsCore(Dollars other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static implicit operator decimal(Dollars dollarAmount)
        {
            return dollarAmount.Value;
        }

        public static explicit operator Dollars(decimal dollarAmount)
        {
            return Create(dollarAmount).Value;
        }
    }
}
