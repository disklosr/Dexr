using Dex.Core.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace Dex.Scaper
{
    public class Program
    {
        private const string DesktopPath = @"C:\Users\pirha\Desktop\";
        private const string UrlToParse = @"http://www.serebii.net/pokemongo/pokemon.shtml";
        private static List<Pokemon> Pokemons;

        private static void Main()
        {
            ParseDataFromUrl();
            //ParseDataFromFile();
            ParseDataFromJsonFile();
            WriteModelToFile(Pokemons);
        }

        private static void ParseDataFromFile()
        {
            var str = File.ReadAllText("in.txt");
            var parser = new EvolutionsParser();
            parser.Parse(str);
            parser.MergeWith(Pokemons);
        }

        private static void ParseDataFromJsonFile()
        {
            var str = File.ReadAllText("types.json");
            var parser = new JsonEvolutionParser();
            parser.Parse(str);
            parser.MergeWith(Pokemons);
        }

        private static void ParseDataFromUrl()
        {
            HttpClient client = new HttpClient();
            var html = client.GetStringAsync(UrlToParse).Result;
            Pokemons = new Parser().ParseHtml(html);
        }

        private static void WriteModelToFile(List<Pokemon> model)
        {
            var json = JsonConvert.SerializeObject(model, Formatting.Indented);
            File.WriteAllText(Path.Combine(DesktopPath, "pokemons.db.json"), json, Encoding.UTF8);
        }
    }
}