namespace Dex.Core.Entities
{
    public class Attack : CombatStat
    {
        public Attack(ushort value)
        {
            Value = value;
        }
    }

    public abstract class CombatStat
    {
        public ushort Value { get; protected set; }
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