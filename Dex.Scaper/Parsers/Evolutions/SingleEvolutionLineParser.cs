using Dex.Scaper.Utils;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dex.Scaper.Parsers.Evolutions
{
    public class SingleEvolutionLineParser : IParser<ushort[]>
    {
        public ushort[] Parse(string htmlRow)
        {
            List<ushort> dexNumbers = new List<ushort>();

            Regex rgx = new Regex("#([0-9]{3})");

            var matches = rgx.Matches(HtmlUtils.HtmlDecode(htmlRow));

            foreach (Match match in matches)
            {
                dexNumbers.Add(ushort.Parse(match.Groups[1].ToString()));
            }

            return dexNumbers.ToArray();
        }
    }
}