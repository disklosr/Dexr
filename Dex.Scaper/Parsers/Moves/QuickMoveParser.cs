using Dex.Core.Entities;
using Dex.Scaper.Utils;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dex.Scaper.Parsers
{
    public class QuickMoveParser : IParser<QuickMove>
    {
        public QuickMove Parse(string htmlSingleRowInput)
        {
            var nameNode = HtmlUtils.GetSingleHtmlNode(htmlSingleRowInput, "./tr/td[1]");
            var typeNode = HtmlUtils.GetSingleHtmlNode(htmlSingleRowInput, "./tr/td[2]//img");
            var DamageNode = HtmlUtils.GetSingleHtmlNode(htmlSingleRowInput, "./tr/td[3]");
            var EnergyIncreaseNode = HtmlUtils.GetSingleHtmlNode(htmlSingleRowInput, "./tr/td[4]");
            var CoolDownNode = HtmlUtils.GetSingleHtmlNode(htmlSingleRowInput, "./tr/td[5]");

            var quickMove = new QuickMove();

            //Name
            quickMove.Name = nameNode.InnerText.Trim();
            quickMove.MoveId = string.Join("-", quickMove.Name.Split(new char[] { '-', ' ' }).Select(s => s.ToLowerInvariant())).Trim();

            //Type
            var src = typeNode.GetAttributeValue("src", string.Empty);
            var typeAsText = Regex.Match(src, @"/([A-Za-z]+?)\.gif").Groups[1].Value;
            quickMove.Type = (PokemonType)Enum.Parse(typeof(PokemonType), typeAsText, true);

            //Damage
            quickMove.Damage = ushort.Parse(DamageNode.InnerText);

            //CoolDown
            quickMove.CoolDown = float.Parse(CoolDownNode.InnerText.Replace(" seconds", string.Empty));

            return quickMove;
        }
    }
}