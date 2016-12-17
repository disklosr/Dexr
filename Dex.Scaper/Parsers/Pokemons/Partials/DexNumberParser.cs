using Dex.Scaper.Utils;
using System.Text.RegularExpressions;

namespace Dex.Scaper.Parsers
{
    public class DexNumberParser : IParser<ushort>
    {
        public ushort Parse(string htmlSingleRowInput)
        {
            var nameColumn = HtmlUtils.GetSingleHtmlNode(htmlSingleRowInput, "./tr/td[1]");
            var rowContent = nameColumn.InnerText;
            var match = Regex.Match(rowContent, "[0-9]{3}").Value;
            return ushort.Parse(match);
        }
    }
}