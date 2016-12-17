using Dex.Core.Entities;

namespace Dex.Scaper.Parsers
{
    public interface IParser<T>
    {
        T Parse(string htmlSingleRowInput);
    }
}