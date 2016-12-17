using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dex.Scaper.Sources
{
    public class PokemonsParserSource : IParserSource
    {
        private const string HtmlSource = "http://www.serebii.net/pokemongo/pokemon.shtml";
        private readonly string cacheFilePath = Path.Combine(Directory.GetCurrentDirectory(), "pokemons.shtml");

        public async Task<string> GetStringAsync()
        {
            if (File.Exists(cacheFilePath))
                return File.ReadAllText(cacheFilePath);

            HttpClient client = new HttpClient();
            var htmlString = await client.GetStringAsync(HtmlSource);
            File.WriteAllText(cacheFilePath, htmlString);

            return htmlString;
        }

        private bool IsCacheFilePresent(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            return File.Exists(filePath);
        }
    }
}