namespace Dex.Core.Entities
{
    public class Attack : CombatStat
    {
        public Attack(ushort attack)
        {
            Value = attack;
        }
    }

    public abstract class CombatStat
    {
        public ushort Value { get; protected set; }
    }

    public class Defense : CombatStat
    {
        public Defense(ushort defense)
        {
            Value = defense;
        }
    }

    public class Stamina : CombatStat
    {
        public Stamina(ushort stamina)
        {
            Value = stamina;
        }
    }
}