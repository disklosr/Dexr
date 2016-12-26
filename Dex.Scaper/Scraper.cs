using Dex.Core.Entities;
using Dex.Scaper.Factories;
using Dex.Scaper.Parsers;
using Dex.Scaper.Parsers.Evolutions;
using Dex.Scaper.Sources;
using Dex.Scaper.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace Dex.Scaper
{
    public class Scraper
    {
        private EvolutionsParser _evolutionsParser;
        private List<ushort[]> _parsedEvolutions;
        private List<Pokemon> _parsedPokemons;
        private PokemonsParser _pokemonsParser;

        public void Scrape()
        {
            //ParsePokemons();
            ParseEvolutionLines();
        }

        private void ParseEvolutionLines()
        {
            var evolutionsSource = new PokemonsEvolutionsSource();
            _evolutionsParser = EvolutionsParserFactory.CreateEvolutionsParser();
            _parsedEvolutions = _evolutionsParser.Parse(evolutionsSource.GetStringAsync().Result);
            var json = JsonUtils.ToJson(_parsedEvolutions);
            WriteJsonToFile(json, "Evolutions");
        }

        private void ParseMoves()
        {
            throw new NotImplementedException();
        }

        private void ParsePokemons()
        {
            var pokemonsSource = new PokemonsParserSource();
            _pokemonsParser = PokemonsParserFactory.CreatePokemonsParser();
            _parsedPokemons = _pokemonsParser.Parse(pokemonsSource.GetStringAsync().Result);
            var json = JsonUtils.ToJson(_parsedPokemons);
            WriteJsonToFile(json, "Pokemons");
        }

        private void WriteJsonToFile(string json, string name)
        {
            var fileName = name + DateTime.Now.ToString("hhmmss") + ".json";
            var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            File.WriteAllText(path, json);

            EnvUtils.OpenJsonFile(fileName);
        }
    }
}