namespace Dex.Scaper.Sources
{
    public class PokemonsEvolutionsSource : ParserSourceBase, IParserSource
    {
        protected override string CacheFileName => "evolutions.shtml";

        protected override string HtmlSource => "http://www.pokemongoevolution.com/";
    }
}