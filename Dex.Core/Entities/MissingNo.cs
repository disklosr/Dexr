using System.Linq;

namespace Dex.Core.Entities
{
    public sealed class MissingNo : Pokemon
    {
        public MissingNo()
        {
            DexNumber = ushort.MinValue;
            Name = "MissingNo";
            Type1 = Type.Unknown;
            Type2 = Type.Unknown;

            Attack = new Attack(0);
            Defense = new Defense(0);
            Stamina = new Stamina(0);

            Moves = new PokemonMovesIds()
            {
                QuickMovesIds = Enumerable.Empty<string>(),
                ChargeMovesIds = Enumerable.Empty<string>()
            };
        }
    }
}