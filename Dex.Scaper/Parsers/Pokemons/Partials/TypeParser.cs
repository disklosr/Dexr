using Dex.Scaper.Utils;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using PokemonType = Dex.Core.Entities.PokemonType;

namespace Dex.Scaper.Parsers
{
    public class TypeParser : IParser<PokemonType[]>
    {
        public PokemonType[] Parse(string htmlSingleRowInput)
        {
            var nameColumn = HtmlUtils.GetSingleHtmlNode(htmlSingleRowInput, "./tr/td[4]");
            var hrefs = nameColumn.SelectNodes(".//a").Select(node => node.GetAttributeValue("href", null));

            return hrefs.Select(href =>
            {
                var typeAsText = Regex.Match(href, @"/([A-Za-z]+?)\.shtml").Groups[1].Value;
                return (PokemonType)Enum.Parse(typeof(PokemonType), typeAsText, true);
            }).ToArray();
        }
    }
}