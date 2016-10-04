namespace Dex.Core.Entities
{
    public class Attack : PokemonStat
    {
        public Attack(ushort value)
        {
            Value = value;
        }
    }

    public class Defense : PokemonStat
    {
        public Defense(ushort value)
        {
            Value = value;
        }
    }

    public abstract class PokemonStat
    {
        public ushort Value { get; protected set; }
    }

    public class Stamina : PokemonStat
    {
        public Stamina(ushort value)
        {
            Value = value;
        }
    }
}