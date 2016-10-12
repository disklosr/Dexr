using Dex.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dex.Scaper
{
    public class EvolutionsParser
    {
        private List<Tuple<string, string, string>> Corrections;

        public void MergeWith(List<Pokemon> pokemons)
        {
            pokemons.ForEach(pokemon =>
            {
                pokemon.EvolvesFrom = Corrections.Where(tuple => tuple.Item1 == pokemon.DexNumber.ToString()).First().Item2;
                pokemon.EvolvesTo = Corrections.Where(tuple => tuple.Item1 == pokemon.DexNumber.ToString()).First().Item3;
            });
        }

        public void Parse(string input)
        {
            var list = new List<Tuple<string, string, string>>();

            StringReader reader = new StringReader(input);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var tokens = Regex.Split(line, @"\s+").Where(s => s != string.Empty).ToArray();
                list.Add(new Tuple<string, string, string>(tokens[0].ToLowerInvariant(), tokens[1].ToLowerInvariant(), tokens[2].ToLowerInvariant()));
            }

            Corrections = list.Select(tuple =>
            {
                string evolvesFrom = null;
                string evolvesTo = null;

                if (tuple.Item2.Contains("/"))
                {
                    var tokens = Regex.Split(tuple.Item2, @"/|\.").Where(s => s != string.Empty).ToArray();
                    evolvesFrom = tokens[5];
                }

                if (tuple.Item3.Contains("/"))
                {
                    var tokens = Regex.Split(tuple.Item3, @"/|\.").Where(s => s != string.Empty).ToArray();
                    evolvesTo = tokens[5];
                }

                return new Tuple<string, string, string>(tuple.Item1, evolvesFrom, evolvesTo);
            }).ToList();
        }
    }
}