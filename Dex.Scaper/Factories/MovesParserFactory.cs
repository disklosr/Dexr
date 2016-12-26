using Dex.Scaper.Parsers;

namespace Dex.Scaper.Factories
{
    public class MovesParserFactory
    {
        public static MovesParser CreateMovesParser()
        {
            var quickMoveParser = new QuickMoveParser();
            var chargeMoveParser = new ChargeMoveParser();
            return new MovesParser(quickMoveParser, chargeMoveParser);
        }
    }
}