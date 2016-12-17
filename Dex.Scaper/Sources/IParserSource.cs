using System.Threading.Tasks;

namespace Dex.Scaper.Sources
{
    public interface IParserSource
    {
        Task<string> GetStringAsync();
    }
}