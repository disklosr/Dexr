using Dex.Uwp.Infrastructure;
using Windows.ApplicationModel;

namespace Dex.Uwp.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        private readonly PackageVersion _appVersion;

        public AboutViewModel()
        {
            _appVersion = Package.Current.Id.Version;
        }
        public string AppVersion => string.Format("{0}.{1}.{2}.{3}", _appVersion.Major, _appVersion.Minor, _appVersion.Build, _appVersion.Revision);
    }
}