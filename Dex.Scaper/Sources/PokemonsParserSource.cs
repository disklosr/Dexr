namespace Dex.Scaper.Sources
{
    public class PokemonsParserSource : ParserSourceBase, IParserSource
    {
        protected override string CacheFileName => "pokemons.shtml";

        protected override string HtmlSource => "http://www.serebii.net/pokemongo/pokemon.shtml";
    }
}