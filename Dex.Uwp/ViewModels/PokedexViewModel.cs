using Dex.Core.Entities;
using Dex.Core.Repositories;
using Dex.Uwp.Infrastructure;
using Dex.Uwp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;

namespace Dex.Uwp.ViewModels
{
    public class PokedexViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private readonly IPokemonRepository pokemonsRepository;

        private IEnumerable<Pokemon> allPokemonsByCp;
        private IEnumerable<Pokemon> allPokemonsByDexNumber;
        private IEnumerable<Pokemon> allPokemonsByName;
        private IEnumerable<Pokemon> allPokemonsByType;
        private IEnumerable<Pokemon> allPokemonsCache;

        public PokedexViewModel(IPokemonRepository pokemonsRepository, INavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.pokemonsRepository = pokemonsRepository;

            ReverseOrderCommand = new RelayCommand(() => OnReverseOrder());
            SearchCommand = new RelayCommand(() => navigationService.NavigateToSearchPage());
        }

        public IEnumerable<Pokemon> AllPokemonsByCp
        {
            get { return allPokemonsByCp; }
            private set { Set(ref allPokemonsByCp, value); }
        }

        public IEnumerable<Pokemon> AllPokemonsByDexNumber
        {
            get { return allPokemonsByDexNumber; }
            private set { Set(ref allPokemonsByDexNumber, value); }
        }

        public IEnumerable<Pokemon> AllPokemonsByName
        {
            get { return allPokemonsByName; }
            private set { Set(ref allPokemonsByName, value); }
        }

        public IEnumerable<Pokemon> AllPokemonsByType
        {
            get { return allPokemonsByType; }
            private set { Set(ref allPokemonsByType, value); }
        }

        public ICommand ReverseOrderCommand { get; }
        public ICommand SearchCommand { get; }

        public async override Task OnNavigatedTo(NavigationEventArgs e)
        {
            var pokes = pokemonsRepository.GetAllPokemons();
            allPokemonsCache = await pokes;

            AllPokemonsByDexNumber = allPokemonsCache.OrderBy(poke => poke.DexNumber);
            AllPokemonsByCp = allPokemonsCache.OrderBy(poke => poke.MaxCP);
            AllPokemonsByName = allPokemonsCache.OrderBy(poke => poke.Name);
            AllPokemonsByType = allPokemonsCache.OrderBy(poke => poke.Type1).ThenBy(poke => poke.Type2);
        }

        public void OnSelectNewItem(Pokemon selectedPoke)
        {
            navigationService.NavigateToPokemonDetailsPage(selectedPoke.DexNumber);
        }

        private void OnReverseOrder()
        {
            AllPokemonsByCp = AllPokemonsByCp.Reverse();
            AllPokemonsByDexNumber = AllPokemonsByDexNumber.Reverse();
            AllPokemonsByName = AllPokemonsByName.Reverse();
            AllPokemonsByType = AllPokemonsByType.Reverse();
        }
    }
}