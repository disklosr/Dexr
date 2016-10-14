using Dex.Core.Entities;
using Dex.Core.Repositories;
using Dex.Uwp.Infrastructure;
using Dex.Uwp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace Dex.Uwp.ViewModels
{
    public class PokedexViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private readonly IPokemonRepository pokemonsRepository;

        private IEnumerable<Pokemon> allPokemons;
        private Pokemon selectedPokemon;

        public PokedexViewModel(IPokemonRepository pokemonsRepository, INavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.pokemonsRepository = pokemonsRepository;
        }

        public IEnumerable<Pokemon> AllPokemons
        {
            get { return allPokemons; }
            private set { Set(ref allPokemons, value); }
        }

        public Pokemon SelectedPokemon
        {
            get { return selectedPokemon; }
            set
            {
                Set(ref selectedPokemon, value);
                navigationService.NavigateToPokemonDetailsPage(value.DexNumber);
            }
        }

        public async override Task OnNavigatedTo(NavigationEventArgs e)
        {
            var pokes = await pokemonsRepository.GetAllPokemons();
            AllPokemons = pokes;
            SelectedPokemon = null;
        }
    }
}