using System;
using System.IO.Compression;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace Dex.Uwp.Services
{
    public interface IAppLifecycleManager
    {
        Task<bool> IsFirstRun();

        Task InitializeAppForFirstRun();
    }

    public class AppLifecycleManager : IAppLifecycleManager
    {
        IEncryptionService _encryptionService;

        public AppLifecycleManager(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }
        public async Task<bool> IsFirstRun()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var picturesFolder = await localFolder.TryGetItemAsync("pks");
            return picturesFolder == null;
        }

        public async Task InitializeAppForFirstRun()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var appInstalledFolder = Package.Current.InstalledLocation;
            var zipFile = await appInstalledFolder.GetFileAsync(@"Assets\pks.zip");
            
            //Don't care when this finishes processing
            Task.Run(() => ZipFile.ExtractToDirectory(zipFile.Path, localFolder.Path));
        }
    }
}
