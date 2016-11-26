using Dex.Uwp.Infrastructure;
using Dex.Uwp.Services;
using System.Threading.Tasks;

namespace Dex.Uwp.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly IPokePicturesManager picturesManager;

        public SettingsViewModel(IPokePicturesManager picturesManager)
        {
            this.picturesManager = picturesManager;
        }

        private async Task OnDownloadNewImages()
        {
            await picturesManager.InitPokemonPictures();
        }
    }
}