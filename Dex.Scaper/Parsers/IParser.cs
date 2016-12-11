namespace Dex.Scaper.Parsers
{
    public interface IParser<T>
    {
        T Parse(string htmlInput);
    }
}