using Dex.Core.Entities;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dex.Scaper
{
    public class Parser
    {
        public List<Pokemon> ParseHtml(string html)
        {
            string baseXPath = "//table[@class='tab' and @align='center']";
            var document = new HtmlDocument();
            document.LoadHtml(html);
            List<Pokemon> pokemons = new List<Pokemon>();

            var table = document.DocumentNode.SelectNodes(baseXPath);
            var rows = table[1].SelectNodes("./tr");

            for (ushort i = 1; i < rows.Count; i++)
            {
                var poke = new Pokemon();

                GetStats(rows[i], poke);

                GetNumber(i, poke);

                GetName(rows[i], poke);

                GetCandies(rows[i], poke);

                GetTypes(rows[i], poke);

                GetFastMoves(rows[i], poke);

                GetSpecialMoves(rows[i], poke);

                GetEggDistance(rows[i], poke);

                pokemons.Add(poke);
            }

            return pokemons;
        }

        private void GetCandies(HtmlNode row, Pokemon poke)
        {
            var candies = HtmlEntity.DeEntitize(row.SelectSingleNode("./td[6]").InnerText);
            var parsed = candies.Contains("None") ? "0" : Regex.Match(candies, @"([0-9]+) ?.+?Candy").Groups[1].Value;
            poke.CandiesToEvolve = parsed == "None" ? (ushort)0 : ushort.Parse(parsed);
        }

        private void GetEggDistance(HtmlNode row, Pokemon poke)
        {
            var text = HtmlEntity.DeEntitize(row.SelectSingleNode("./td[7]").InnerText);
            if (text.Contains("Not")) poke.EggDistance = 0;
            else
            {
                poke.EggDistance = ushort.Parse(Regex.Match(text, @"([0-9]+)").Groups[1].Value);
            }
        }

        private void GetFastMoves(HtmlNode row, Pokemon poke)
        {
            var moves = row.SelectNodes("./td[8]//u");
            var fastMoves = new List<Move>();

            for (int j = 1; j <= moves.Count; j++)
            {
                fastMoves.Add(new Move() { Name = moves[j - 1].InnerText.ToLowerInvariant() });
            }

            poke.QuickMoves = fastMoves;
        }

        private void GetName(HtmlNode row, Pokemon poke)
        {
            poke.Name = HtmlEntity.DeEntitize(HtmlEntity.DeEntitize(row.SelectSingleNode("./td[3]/a").InnerText)).ToLowerInvariant();
        }

        private void GetNumber(ushort i, Pokemon poke)
        {
            poke.DexNumber = i;
        }

        private void GetSpecialMoves(HtmlNode row, Pokemon poke)
        {
            var moves = row.SelectNodes("./td[9]//u");
            var specialMoves = new List<Move>();
            for (int j = 1; j <= moves.Count; j++)
            {
                specialMoves.Add(new Move() { Name = moves[j - 1].InnerText.ToLowerInvariant() });
            }

            poke.SpecialMoves = specialMoves;
        }

        private void GetStats(HtmlNode row, Pokemon poke)
        {
            poke.Stamina = new Stamina(ushort.Parse(row.SelectSingleNode("./td[5]//tr[1]//td[2]").InnerText));
            poke.Attack = new Attack(ushort.Parse(row.SelectSingleNode("./td[5]//tr[2]//td[2]").InnerText));
            poke.Defense = new Defense(ushort.Parse(row.SelectSingleNode("./td[5]//tr[3]//td[2]").InnerText));

            poke.MaxCP = ushort.Parse(row.SelectSingleNode("./td[5]//tr[4]//td[2]").InnerText);
            poke.CatchRate = ushort.Parse(row.SelectSingleNode("./td[5]//tr[5]//td[2]").InnerText.Replace("%", string.Empty));
            poke.FleeRate = ushort.Parse(row.SelectSingleNode("./td[5]//tr[6]//td[2]").InnerText.Replace("%", string.Empty));
        }

        private void GetTypes(HtmlNode row, Pokemon poke)
        {
            string type1, type2;

            var types = row.SelectNodes("./td[4]//img");

            type1 = Regex.Match(types[0].GetAttributeValue("src", null), @"type/(.+).gif").Groups[1].Value;
            poke.Type1 = (Core.Entities.Type)Enum.Parse(typeof(Core.Entities.Type), type1, true);

            if (types.Count == 2)
            {
                type2 = Regex.Match(types[1].GetAttributeValue("src", null), @"type/(.+).gif").Groups[1].Value;
                poke.Type2 = (Core.Entities.Type)Enum.Parse(typeof(Core.Entities.Type), type2, true);
            }
        }
    }
}