using Dex.Scaper.Utils;

namespace Dex.Scaper.Parsers
{
    public class NameParser : IParser<string>
    {
        public string Parse(string htmlSingleRowInput)
        {
            var nameColumn = HtmlUtils.GetSingleHtmlNode(htmlSingleRowInput, "./tr/td[3]");
            return HtmlUtils.HtmlDecode(nameColumn.SelectSingleNode(".//a").InnerText.ToLower());
        }
    }
}