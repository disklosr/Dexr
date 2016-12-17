using Dex.Core.Entities;
using Dex.Scaper.Factories;
using Dex.Scaper.Parsers;
using Dex.Scaper.Sources;
using Dex.Scaper.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace Dex.Scaper
{
    public class Scraper
    {
        private List<Pokemon> _parsedPokemons;
        private PokemonsParser _pokemonsParser;

        public void Scrape()
        {
            ParsePokemons();
            //ParseMoves();

            var json = JsonUtils.ToJson(_parsedPokemons);
            WriteJsonToFile(json);
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
        }

        private void WriteJsonToFile(string json)
        {
            var fileName = "Pokemons" + DateTime.Now.ToString("hhmmss") + ".json";
            var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            File.WriteAllText(path, json);

            EnvUtils.OpenJsonFile(fileName);
        }
    }
}