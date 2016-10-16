using System;

namespace Dex.Core.Entities
{
    public class Attack : CombatStat
    {
        public Attack(ushort value)
        {
            Value = value;
        }
    }

    public abstract class CombatStat : IComparable<CombatStat>
    {
        public ushort Value { get; protected set; }

        public int CompareTo(CombatStat that)
        {
            return this.Value.CompareTo(that.Value);
        }
    }

    public class Defense : CombatStat
    {
        public Defense(ushort value)
        {
            Value = value;
        }
    }

    public class Stamina : CombatStat
    {
        public Stamina(ushort value)
        {
            Value = value;
        }
    }
}