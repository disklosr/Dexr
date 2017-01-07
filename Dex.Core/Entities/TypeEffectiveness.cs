using System.Collections.Generic;
using System.Linq;

namespace Dex.Core.Entities
{
    public class TypeEffectiveness
    {
        public TypeEffectiveness(IEnumerable<PokemonType> strengths, IEnumerable<PokemonType> weaknesses)
        {
            StrongAgainst = strengths.ToArray();
            WeakAgainst = weaknesses.ToArray();
        }

        public PokemonType[] StrongAgainst { get; }
        public PokemonType[] WeakAgainst { get; }
    }
}