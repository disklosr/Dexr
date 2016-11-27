using Dex.Uwp.Services;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace Dex.Uwp.DataAccess
{

    public interface IPokePicturesSource
    {
        string GetPath(uint dexNumber);

        string Name { get; }
    }

    public interface INeedInitialisation
    {
        Task Initialize();

        Task<bool> IsInitialized();
    }

    public class OfficialPokePicturesSource : IPokePicturesSource, INeedInitialisation
    {
        private readonly string localFolderPath;
        private const string pathTemplate = @"pks\{0}.png";
        IEncryptionService _encryptionService;

        public OfficialPokePicturesSource(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
            localFolderPath = ApplicationData.Current.LocalFolder.Path;
        }
        //TODO: Localize this
        public string Name => "Official";

        public string GetPath(uint dexNumber)
        {
            return Path.Combine(localFolderPath, string.Format(pathTemplate, dexNumber.ToString("D3")));
        }

        public async Task Initialize()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var appInstalledFolder = Package.Current.InstalledLocation;
            var encryptedZipFile = await appInstalledFolder.GetFileAsync(@"Assets\pks.pks");

            var encryptedZipBuffer = await FileIO.ReadBufferAsync(encryptedZipFile);
            var decryptedBuffer = _encryptionService.Decrypt(encryptedZipBuffer, Enumerable.Range(1, 6).Select(i => i.ToString()).Aggregate((i, j) => i + j));

            var newfile = await localFolder.CreateFileAsync("pks.zip");
            await FileIO.WriteBufferAsync(newfile, decryptedBuffer);

            await Task.Run(() => ZipFile.ExtractToDirectory(newfile.Path, localFolder.Path));
            await newfile.DeleteAsync();
        }

        public async Task<bool> IsInitialized()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var picturesFolder = await localFolder.TryGetItemAsync("pks");
            return picturesFolder == null;
        }
    }

    public class DefaultPokePicturesSource : IPokePicturesSource
    {
        //TODO: Localize this
        public string Name => "Default";

        private const string pathTemplate = @"ms-appx:///Assets\Flat\{0}.png";

        public string GetPath(uint dexNumber)
        {
            return string.Format(pathTemplate, dexNumber.ToString("D3"));
        }
    }
}