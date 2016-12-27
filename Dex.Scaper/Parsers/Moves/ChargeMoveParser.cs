using Dex.Core.Entities;
using Dex.Scaper.Utils;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dex.Scaper.Parsers
{
    public class ChargeMoveParser : IParser<ChargeMove>
    {
        public ChargeMove Parse(string htmlSingleRowInput)
        {
            var nameNode = HtmlUtils.GetSingleHtmlNode(htmlSingleRowInput, "./tr/td[1]");
            var typeNode = HtmlUtils.GetSingleHtmlNode(htmlSingleRowInput, "./tr/td[2]//a");
            var DamageNode = HtmlUtils.GetSingleHtmlNode(htmlSingleRowInput, "./tr/td[3]");
            var CriticalHitNode = HtmlUtils.GetSingleHtmlNode(htmlSingleRowInput, "./tr/td[4]");
            var CoolDownNode = HtmlUtils.GetSingleHtmlNode(htmlSingleRowInput, "./tr/td[5]");
            var EnergyBarsNode = HtmlUtils.GetSingleHtmlNode(htmlSingleRowInput, "./tr/td[6]/img");

            var chargeMove = new ChargeMove();

            //Name
            chargeMove.Name = nameNode.InnerText.Trim();
            chargeMove.MoveId = string.Join("-", chargeMove.Name.Split(new char[] { '-', ' ' }).Select(s => s.ToLowerInvariant())).Trim();

            //Type
            var href = typeNode.GetAttributeValue("href", string.Empty);
            var typeAsText = Regex.Match(href, @"/([A-Za-z]+?)\.shtml").Groups[1].Value;
            chargeMove.Type = (PokemonType)Enum.Parse(typeof(PokemonType), typeAsText, true);

            //Damage
            chargeMove.Damage = ushort.Parse(DamageNode.InnerText);

            //CriticalHit
            chargeMove.Critical = ushort.Parse(CriticalHitNode.InnerText.Replace("%", string.Empty));

            //CoolDown
            chargeMove.CoolDown = float.Parse(CoolDownNode.InnerText.Replace(" seconds", string.Empty));

            //EnergyBars
            var src = EnergyBarsNode.GetAttributeValue("src", string.Empty);
            chargeMove.EnergyBars = ushort.Parse(src.Replace("energy.png", string.Empty));

            return chargeMove;
        }
    }
}