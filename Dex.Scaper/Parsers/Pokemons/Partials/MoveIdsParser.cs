using Dex.Core.Entities;
using Dex.Scaper.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Dex.Scaper.Parsers
{
    public class MoveIdsParser : IParser<PokemonMovesIds>
    {
        public PokemonMovesIds Parse(string htmlSingleRowInput)
        {
            var pokemonMovesIds = new PokemonMovesIds();

            pokemonMovesIds.QuickMovesIds = GetMoves(htmlSingleRowInput, "./tr/td[8]");
            pokemonMovesIds.ChargeMovesIds = GetMoves(htmlSingleRowInput, "./tr/td[9]");

            return pokemonMovesIds;
        }

        private IEnumerable<string> GetMoves(string html, string xpath)
        {
            var movesColumn = HtmlUtils.GetSingleHtmlNode(html, xpath);
            var moves = movesColumn.SelectNodes(".//u").Select(item => item.InnerText.ToLowerInvariant());
            return moves.Select(move => string.Join("-", move.Split(' ').ToArray())).ToList();
        }
    }
}