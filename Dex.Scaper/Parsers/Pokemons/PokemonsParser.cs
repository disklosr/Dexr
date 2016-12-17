using Dex.Core.Entities;
using Dex.Scaper.Utils;
using System.Collections.Generic;

namespace Dex.Scaper.Parsers
{
    public class PokemonsParser : IParser<List<Pokemon>>
    {
        private const string baseXPath = "//table[@class='tab' and @align='center' and count(./tr) > 50]/tr";
        private readonly IParser<Pokemon> _singlePokemonParser;

        public PokemonsParser(IParser<Pokemon> singlePokemonParser)
        {
            _singlePokemonParser = singlePokemonParser;
        }

        public List<Pokemon> Parse(string html)
        {
            var pokemonsList = new List<Pokemon>();

            var rows = HtmlUtils.GetHtmlNodes(html, baseXPath);

            for (int i = 1; i < rows.Count; i++)
            {
                var parsedPokemon = _singlePokemonParser.Parse(rows[i].OuterHtml);
                pokemonsList.Add(parsedPokemon);
            }

            return pokemonsList;
        }
    }
}