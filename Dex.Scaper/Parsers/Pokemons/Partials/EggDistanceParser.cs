using Dex.Scaper.Utils;
using System.Text.RegularExpressions;

namespace Dex.Scaper.Parsers
{
    public class EggDistanceParser : IParser<ushort>
    {
        public ushort Parse(string htmlSingleRowInput)
        {
            var candiesColumn = HtmlUtils.GetSingleHtmlNode(htmlSingleRowInput, "./tr/td[7]");
            var rowContent = candiesColumn.InnerText;
            var match = Regex.Match(rowContent, "[0-9]{1,2}").Value;
            return string.IsNullOrEmpty(match) ? (ushort)0 : ushort.Parse(match);
        }
    }
}