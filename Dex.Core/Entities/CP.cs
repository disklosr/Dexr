using Dex.Core.Base;
using Dex.Core.Extensions;
using System;

namespace Dex.Core.Entities
{
    public class CP : ValueObject<CP>
    {
        public CP(double value)
        {
            PreciseValue = value;
            Value = ((ushort)Math.Floor(PreciseValue)).ClipToMin(10);
        }

        public double PreciseValue { get; }

        public ushort Value { get; }

        public override string ToString()
        {
            return Value.ToString();
        }

        protected override bool EqualsCore(CP other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}