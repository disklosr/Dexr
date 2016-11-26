using Dex.Uwp.DataAccess;
using Dex.Uwp.Infrastructure;
using System.Threading.Tasks;

namespace Dex.Uwp.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly IPokePicturesSourceProvider _picturesSourceProvider;

        public SettingsViewModel(IPokePicturesSourceProvider picturesSourceProvider)
        {
            _picturesSourceProvider = picturesSourceProvider;
            AvailableSources = _picturesSourceProvider.AvailableSources;
        }

        private Task OnDownloadNewImages()
        {
            return Task.CompletedTask;
        }

        public IPokePicturesSource[] AvailableSources { get; }

        public IPokePicturesSource SelectedSource
        {
            get { return _picturesSourceProvider.Source; }
            set { _picturesSourceProvider.Source = value; }
        }
    }
}