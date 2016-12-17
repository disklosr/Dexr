using Dex.Scaper.Parsers;
using Dex.Scaper.Parsers.Pokemons;

namespace Dex.Scaper.Factories
{
    public class PokemonsParserFactory
    {
        public static PokemonsParser CreatePokemonsParser()
        {
            var singlePokemonParser = new SinglePokemonParser(new PokemonParsers());
            return new PokemonsParser(singlePokemonParser);
        }
    }
}