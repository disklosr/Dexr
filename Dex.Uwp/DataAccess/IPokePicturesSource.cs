using System.IO;
using Windows.Storage;

namespace Dex.Uwp.DataAccess
{

    public interface IPokePicturesSource
    {
        string GetPath(uint dexNumber);

        string Name { get; }
    }

    public class OfficialPokePicturesSource : IPokePicturesSource
    {
        private readonly string localFolderPath;
        private const string pathTemplate = @"pks\{0}.png";

        //TODO: Localize this
        public string Name => "Official";

        public OfficialPokePicturesSource()
        {
            localFolderPath = ApplicationData.Current.LocalFolder.Path;
        }

        public string GetPath(uint dexNumber)
        {
            return Path.Combine(localFolderPath, string.Format(pathTemplate, dexNumber.ToString("D3")));
        }
    }

    public class DefaultPokePicturesSource : IPokePicturesSource
    {
        //TODO: Localize this
        public string Name => "Default";

        private const string filePath = @"ms-appx:///Assets\DefaultPoke.png";

        public string GetPath(uint dexNumber)
        {
            return filePath;
        }
    }
}