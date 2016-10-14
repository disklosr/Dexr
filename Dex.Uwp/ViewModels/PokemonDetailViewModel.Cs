using Dex.Core.Entities;
using Dex.Core.Repositories;
using Dex.Uwp.Infrastructure;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace Dex.Uwp.ViewModels
{
    public class PokemonDetailViewModel : ViewModelBase
    {
        private readonly IPokemonRepository pokemonRepository;

        private Pokemon selectedPokemon;

        public PokemonDetailViewModel(IPokemonRepository pokemonRepository)
        {
            this.pokemonRepository = pokemonRepository;
        }

        public Pokemon SelectedPokemon
        {
            get { return selectedPokemon; }
            private set { Set(ref selectedPokemon, value); }
        }

        public async override Task OnNavigatedTo(NavigationEventArgs e)
        {
            var dexId = (ushort)e.Parameter;
            SelectedPokemon = await pokemonRepository.GetPokemonById(dexId);
        }
    }
}