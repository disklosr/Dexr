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
        private readonly IMoveRepository moveRepository;
        private readonly INavigationService navigationService;
        private readonly IPokemonRepository pokemonsRepository;
        private IEnumerable<Move> allMoves;
        private IEnumerable<Move> allMovesCache;
        private IEnumerable<Pokemon> allPokemons;

        private IEnumerable<Pokemon> allPokemonsCache;
        private Pokemon selectedPokemon;

        public PokedexViewModel(IPokemonRepository pokemonsRepository, IMoveRepository moveRepository, INavigationService navigationService)
        {
            this.moveRepository = moveRepository;
            this.navigationService = navigationService;
            this.pokemonsRepository = pokemonsRepository;

            SortByNameCommand = new RelayCommand(() => AllPokemons = allPokemonsCache.OrderBy(poke => poke.Name));
            SortByDexNumberCommand = new RelayCommand(() => AllPokemons = allPokemonsCache.OrderBy(poke => poke.DexNumber));
            SortByTypeCommand = new RelayCommand(() => AllPokemons = allPokemonsCache.OrderBy(poke => poke.Type1).ThenBy(poke => poke.Type2));
        }

        public IEnumerable<Move> AllMoves
        {
            get { return allMoves; }
            private set { Set(ref allMoves, value); }
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
                if (value != null)
                    navigationService.NavigateToPokemonDetailsPage(value.DexNumber);
            }
        }

        public ICommand SortByDexNumberCommand { get; }
        public ICommand SortByNameCommand { get; }
        public ICommand SortByTypeCommand { get; }

        public async override Task OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
                SelectedPokemon = null;

            var pokes = pokemonsRepository.GetAllPokemons();
            var moves = moveRepository.GetAllMoves();
            AllPokemons = allPokemonsCache = await pokes;
            var loadedMoves = await moves;
            AllMoves = allMovesCache = loadedMoves.ChargeMoves.Union<Move>(loadedMoves.QuickMoves);
        }
    }
}