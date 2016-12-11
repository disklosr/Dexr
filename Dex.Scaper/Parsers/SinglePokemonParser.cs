using Dex.Core.Entities;

namespace Dex.Scaper.Parsers
{
    public class SinglePokemonParser : IParser<Pokemon>
    {
        private readonly IParser<ushort> _catchRateParser;
        private readonly IParser<ushort> _dexNumberParser;
        private readonly IParser<ushort> _eggDistanceParser;
        private readonly IParser<PokemonMovesIds> _movesParser;
        private readonly IParser<string> _nameParser;
        private IParser<ushort> _candiesParser;

        public SinglePokemonParser(
            IParser<string> nameParser,
            IParser<ushort> candiesParser,
            IParser<ushort> catchRateParser,
            IParser<ushort> dexNumberParser,
            IParser<ushort> eggDistanceParser,
            IParser<PokemonMovesIds> movesParser
            )
        {
            _movesParser = movesParser;
            _eggDistanceParser = eggDistanceParser;
            _dexNumberParser = dexNumberParser;
            _catchRateParser = catchRateParser;
            _candiesParser = candiesParser;
            _nameParser = nameParser;
        }

        public Pokemon Parse(string htmlRow)
        {
            var pokemon = new Pokemon();
            pokemon.Name = _nameParser.Parse(htmlRow);
            pokemon.CandiesToEvolve = _candiesParser.Parse(htmlRow);
            pokemon.CatchRate = _catchRateParser.Parse(htmlRow);
            pokemon.DexNumber = _dexNumberParser.Parse(htmlRow);
            pokemon.EggDistance = _eggDistanceParser.Parse(htmlRow);
            pokemon.Moves = _movesParser.Parse(htmlRow);

            return pokemon;
        }
    }
}