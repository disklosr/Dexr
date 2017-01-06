using Microsoft.Services.Store.Engagement;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.System;

namespace Dex.Uwp.Services
{
    public interface IStoreService
    {
        bool CanSendFeedback { get; }

        Task GiveFeedBack();

        Task RateThisApp();
    }

    public class StoreService : IStoreService
    {
        private readonly string _familyName = Package.Current.Id.FamilyName;

        public bool CanSendFeedback => StoreServicesFeedbackLauncher.IsSupported();

        public Task GiveFeedBack()
        {
            var launcher = StoreServicesFeedbackLauncher.GetDefault();
            return launcher.LaunchAsync().AsTask();
        }

        public Task RateThisApp()
        {
            var uri = new Uri($"ms-windows-store:REVIEW?PFN={_familyName}");
            return Launcher.LaunchUriAsync(uri).AsTask();
        }
    }
}