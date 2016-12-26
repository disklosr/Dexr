using Dex.Core.Entities;
using Dex.Scaper.Utils;
using System.Linq;

namespace Dex.Scaper.Parsers
{
    public class MovesParser : IParser<PokemonMoves>
    {
        private const string BaseXpathChargeMoves = "(//table[@class='tab' and @align='center' and count(./tr) > 20])[2]/tr";
        private const string BaseXpathQuickMoves = "(//table[@class='tab' and @align='center' and count(./tr) > 20])[1]/tr";
        private readonly IParser<ChargeMove> _chargeMovesParser;
        private readonly IParser<QuickMove> _quickMovesParser;

        public MovesParser(IParser<QuickMove> quickMovesParser, IParser<ChargeMove> chargeMovesParser)
        {
            _chargeMovesParser = chargeMovesParser;
            _quickMovesParser = quickMovesParser;
        }

        public PokemonMoves Parse(string htmlInput)
        {
            var quickMovesRows = HtmlUtils.GetHtmlNodes(htmlInput, BaseXpathQuickMoves);
            var chargeMovesRows = HtmlUtils.GetHtmlNodes(htmlInput, BaseXpathChargeMoves);

            var moves = new PokemonMoves();

            moves.QuickMoves = quickMovesRows.ToList()
                .Skip(1)
                .Select(node => _quickMovesParser.Parse(node.OuterHtml))
                .ToList();

            moves.ChargeMoves = chargeMovesRows.ToList()
                .Skip(1)
                .Select(node => _chargeMovesParser.Parse(node.OuterHtml))
                .ToList();

            return moves;
        }
    }
}