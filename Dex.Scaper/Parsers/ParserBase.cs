using HtmlAgilityPack;

namespace Dex.Scaper.Parsers
{
    public class ParserBase
    {
        public HtmlNode LoadNodeFromHtmlString(string html)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            return htmlDocument.DocumentNode.FirstChild;
        }
    }
}