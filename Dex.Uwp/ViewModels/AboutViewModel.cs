using System;
using Dex.Uwp.Infrastructure;
using Microsoft.Services.Store.Engagement;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel;

namespace Dex.Uwp.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        private readonly PackageVersion _appVersion;

        public AboutViewModel()
        {
            _appVersion = Package.Current.Id.Version;
            SendFeedbackCommand = new RelayCommand(async () => await OnSendFeedback());
        }
        public string AppVersion => string.Format("{0}.{1}.{2}.{3}", _appVersion.Major, _appVersion.Minor, _appVersion.Build, _appVersion.Revision);

        public ICommand SendFeedbackCommand { get; }

        private async Task OnSendFeedback()
        {
            var launcher = StoreServicesFeedbackLauncher.GetDefault();
            await launcher.LaunchAsync();
        }

        public bool CanSendFeedback => StoreServicesFeedbackLauncher.IsSupported();
    }
}