using Dex.Scaper.Parsers;
using Dex.Scaper.Parsers.Evolutions;
using Dex.Scaper.Parsers.Pokemons;

namespace Dex.Scaper.Factories
{
    public class EvolutionsParserFactory
    {
        public static EvolutionsParser CreateEvolutionsParser()
        {
            var singleEvolutionLineParser = new SingleEvolutionLineParser();
            return new EvolutionsParser(singleEvolutionLineParser);
        }
    }

    public class PokemonsParserFactory
    {
        public static PokemonsParser CreatePokemonsParser()
        {
            var singlePokemonParser = new SinglePokemonParser(new PokemonParsers());
            return new PokemonsParser(singlePokemonParser);
        }
    }
}