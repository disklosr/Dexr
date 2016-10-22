using Dex.Core.Entities;
using Dex.Scaper.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Dex.Scaper
{
    public class Program
    {
        private const string DesktopPath = @"C:\Users\pirha\Desktop\";
        private const string UrlToParse = @"http://www.serebii.net/pokemongo/pokemon.shtml";
        private static List<ChargeMove> ChargeMoves;
        private static List<Pokemon> Pokemons;
        private static List<QuickMove> QuickMoves;

        private static void Main()
        {
            ParseDataFromUrl();
            //ParseDataFromFile();
            ParseDataFromJsonFile();
            WriteModelToFile(Pokemons);
            //ParseMovesDataFromFile();
            //WriteMovesDataToFile();
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

        private static void ParseMovesDataFromFile()
        {
            var str = File.ReadAllText("Moves.json");
            var data = JsonConvert.DeserializeObject<System.Collections.Generic.Dictionary<string, MoveDTO>>(str);
            QuickMoves = new List<QuickMove>();
            ChargeMoves = new List<ChargeMove>();
            foreach (var kvp in data)
            {
                if (kvp.Value.MoveType == MoveType.Quick)
                {
                    QuickMove move;
                    move = new QuickMove()
                    {
                        Type = kvp.Value.Type,
                        Attack = kvp.Value.Attack,
                        CoolDown = kvp.Value.CoolDown,
                        Name = kvp.Value.Name,
                        Energy = kvp.Value.Energy,
                        MoveId = kvp.Key
                    };

                    QuickMoves.Add(move);
                }
                else
                {
                    ChargeMove move = new ChargeMove()
                    {
                        Type = kvp.Value.Type,
                        Attack = kvp.Value.Attack,
                        CoolDown = kvp.Value.CoolDown,
                        Name = kvp.Value.Name,
                        MoveId = kvp.Key,
                        Charges = kvp.Value.Charges,
                        Critical = kvp.Value.Critical,
                        Dodge = kvp.Value.Dodge
                    };

                    ChargeMoves.Add(move);
                }
            }
        }

        private static void WriteModelToFile(List<Pokemon> model)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new WritablePropertiesOnlyResolver()
            };
            var json = JsonConvert.SerializeObject(model, Formatting.Indented);
            File.WriteAllText(Path.Combine(DesktopPath, "pokemons.db.json"), json, Encoding.UTF8);
        }

        private static void WriteMovesDataToFile()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new WritablePropertiesOnlyResolver()
            };
            var json = JsonConvert.SerializeObject(new { quickMoves = QuickMoves, chargeMoves = ChargeMoves }, Formatting.Indented);
            File.WriteAllText(Path.Combine(DesktopPath, "moves.db.json"), json, Encoding.UTF8);
        }
    }

    internal class WritablePropertiesOnlyResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(System.Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> props = base.CreateProperties(type, memberSerialization);
            return props.Where(p => p.Writable).ToList();
        }
    }
}