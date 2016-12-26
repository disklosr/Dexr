namespace Dex.Scaper.Sources
{
    public class MovesSource : ParserSourceBase, IParserSource
    {
        protected override string CacheFileName => "moves.shtml";

        protected override string HtmlSource => "http://www.serebii.net/pokemongo/moves.shtml";
    }
}