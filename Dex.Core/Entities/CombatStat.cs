using Dex.Core.Base;
using System;

namespace Dex.Core.Entities
{
    public class CombatStat : ValueObject<CombatStat>, IComparable<CombatStat>
    {
        public CombatStat(ushort baseValue, ushort iv = 0)
        {
            if (iv > 15)
                throw new ArgumentOutOfRangeException($"Iv value ({iv}) is invalid");

            Iv = iv;
            BaseValue = baseValue;
        }

        public ushort BaseValue { get; }
        public ushort Iv { get; }
        public ushort Value => (ushort)(BaseValue + Iv);

        public int CompareTo(CombatStat that)
        {
            return BaseValue.CompareTo(that.BaseValue);
        }

        protected override bool EqualsCore(CombatStat other) => BaseValue == other.BaseValue && Iv == other.Iv;

        protected override int GetHashCodeCore() => BaseValue.GetHashCode() ^ Iv.GetHashCode();
    }
}