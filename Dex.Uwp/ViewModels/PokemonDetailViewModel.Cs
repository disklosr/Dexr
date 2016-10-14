using Dex.Core.Entities;
using Dex.Uwp.Infrastructure;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace Dex.Uwp.ViewModels
{
    public class PokemonDetailViewModel : ViewModelBase
    {
        private Pokemon selectedPokemon;

        public Pokemon SelectedPokemon
        {
            get { return selectedPokemon; }
            private set { Set(ref selectedPokemon, value); }
        }

        public override Task OnNavigatedTo(NavigationEventArgs e)
        {
            SelectedPokemon = (Pokemon)e.Parameter;
            return Task.CompletedTask;
        }
    }
}