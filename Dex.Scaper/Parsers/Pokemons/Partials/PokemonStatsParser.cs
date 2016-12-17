using Dex.Scaper.Utils;

namespace Dex.Scaper.Parsers
{
    public class AttackParser : IParser<ushort>
    {
        public ushort Parse(string htmlSingleRowInput)
        {
            var statsColumn = HtmlUtils.GetSingleHtmlNode(htmlSingleRowInput, "./tr/td[5]");
            var catchRate = statsColumn.SelectSingleNode(".//tr[2]/td[2]").InnerText.ToLower();
            return ushort.Parse(catchRate);
        }
    }

    public class CatchRateParser : IParser<ushort>
    {
        public ushort Parse(string htmlSingleRowInput)
        {
            var statsColumn = HtmlUtils.GetSingleHtmlNode(htmlSingleRowInput, "./tr/td[5]");
            var catchRate = statsColumn.SelectSingleNode(".//tr[5]/td[2]").InnerText.ToLower();
            catchRate = catchRate.Replace("%", string.Empty);
            return ushort.Parse(catchRate);
        }
    }

    public class DefenseParser : IParser<ushort>
    {
        public ushort Parse(string htmlSingleRowInput)
        {
            var statsColumn = HtmlUtils.GetSingleHtmlNode(htmlSingleRowInput, "./tr/td[5]");
            var catchRate = statsColumn.SelectSingleNode(".//tr[3]/td[2]").InnerText.ToLower();
            return ushort.Parse(catchRate);
        }
    }

    public class FleeRateParser : IParser<ushort>
    {
        public ushort Parse(string htmlSingleRowInput)
        {
            var statsColumn = HtmlUtils.GetSingleHtmlNode(htmlSingleRowInput, "./tr/td[5]");
            var catchRate = statsColumn.SelectSingleNode(".//tr[6]/td[2]").InnerText.ToLower();
            catchRate = catchRate.Replace("%", string.Empty);
            return ushort.Parse(catchRate);
        }
    }

    public class StaminaParser : IParser<ushort>
    {
        public ushort Parse(string htmlSingleRowInput)
        {
            var statsColumn = HtmlUtils.GetSingleHtmlNode(htmlSingleRowInput, "./tr/td[5]");
            var catchRate = statsColumn.SelectSingleNode(".//tr[1]/td[2]").InnerText.ToLower();
            return ushort.Parse(catchRate);
        }
    }
}