using Dex.Scaper.Utils;
using System.Collections.Generic;

namespace Dex.Scaper.Parsers.Evolutions
{
    public class EvolutionsParser : IParser<List<ushort[]>>
    {
        private const string baseXPath = "//*[@id='page']/table/tbody/tr";
        private readonly IParser<ushort[]> _singleEvolutionLineParser;

        public EvolutionsParser(IParser<ushort[]> singleEvolutionLineParser)
        {
            _singleEvolutionLineParser = singleEvolutionLineParser;
        }

        public List<ushort[]> Parse(string html)
        {
            var evolutionLines = new List<ushort[]>();

            var rows = HtmlUtils.GetHtmlNodes(html, baseXPath);

            for (int i = 0; i < rows.Count - 1; i++)
            {
                var parsedEvolutionLine = _singleEvolutionLineParser.Parse(rows[i].OuterHtml);
                evolutionLines.Add(parsedEvolutionLine);
            }

            return evolutionLines;
        }
    }
}