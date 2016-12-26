using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dex.Scaper.Sources
{
    public abstract class ParserSourceBase
    {
        protected abstract string CacheFileName { get; }
        protected abstract string HtmlSource { get; }

        public async Task<string> GetStringAsync()
        {
            var cacheFilePath = Path.Combine(Directory.GetCurrentDirectory(), CacheFileName);
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