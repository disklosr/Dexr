using Dex.Core.Entities;
using PokemonType = Dex.Core.Entities.PokemonType;

namespace Dex.Scaper.Parsers.Pokemons
{
    public interface IPokemonParsers
    {
        IParser<ushort> AttackParser { get; }
        IParser<ushort> CandiesParser { get; }
        IParser<ushort> CatchRateParser { get; }
        IParser<ushort> DefenseParser { get; }
        IParser<ushort> DexNumberParser { get; }
        IParser<ushort> EggDistanceParser { get; }
        IParser<ushort> FleeRateParser { get; }
        IParser<PokemonMovesIds> MovesParser { get; }
        IParser<string> NameParser { get; }
        IParser<ushort> StaminaParser { get; }
        IParser<PokemonType[]> TypeParser { get; }
    }

    public class PokemonParsers : IPokemonParsers
    {
        public PokemonParsers()
        {
            AttackParser = new AttackParser();
            CandiesParser = new CandiesParser();
            CatchRateParser = new CatchRateParser();
            DefenseParser = new DefenseParser();
            DexNumberParser = new DexNumberParser();
            EggDistanceParser = new EggDistanceParser();
            FleeRateParser = new FleeRateParser();
            MovesParser = new MoveIdsParser();
            NameParser = new NameParser();
            StaminaParser = new StaminaParser();
            TypeParser = new TypeParser();
        }

        public IParser<ushort> AttackParser { get; }

        public IParser<ushort> CandiesParser { get; }

        public IParser<ushort> CatchRateParser { get; }

        public IParser<ushort> DefenseParser { get; }

        public IParser<ushort> DexNumberParser { get; }

        public IParser<ushort> EggDistanceParser { get; }

        public IParser<ushort> FleeRateParser { get; }

        public IParser<PokemonMovesIds> MovesParser { get; }

        public IParser<string> NameParser { get; }

        public IParser<ushort> StaminaParser { get; }

        public IParser<PokemonType[]> TypeParser { get; }
    }
}