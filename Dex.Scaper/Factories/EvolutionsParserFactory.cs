using Dex.Scaper.Parsers.Evolutions;

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
}