using HtmlAgilityPack;
using System.Web;

namespace Dex.Scaper.Utils
{
    public class HtmlUtils
    {
        public static HtmlNodeCollection GetHtmlNodes(string html, string xpath)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);
            return document.DocumentNode.SelectNodes(xpath);
        }

        public static HtmlNode GetHtmlRootNode(string html)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);
            return document.DocumentNode;
        }

        public static HtmlNode GetSingleHtmlNode(string html, string xpath)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);
            return document.DocumentNode.SelectSingleNode(xpath);
        }

        public static string HtmlDecode(string htmlEncoded)
        {
            return HttpUtility.HtmlDecode(htmlEncoded);
        }
    }
}