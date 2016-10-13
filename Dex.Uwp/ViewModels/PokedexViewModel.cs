using Dex.Core.Entities;
using Dex.Core.Repositories;
using Dex.Uwp.Infrastructure;
using System.Collections.Generic;
using Windows.UI.Xaml.Navigation;

namespace Dex.Uwp.ViewModels
{
    public class PokedexViewModel : ViewModelBase
    {
        private readonly IPokemonRepository pokemonsRepository;

        private IEnumerable<Pokemon> allPokemons;

        public PokedexViewModel(IPokemonRepository pokemonsRepository)
        {
            this.pokemonsRepository = pokemonsRepository;
        }

        public IEnumerable<Pokemon> AllPokemons
        {
            get { return allPokemons; }
            private set { Set(ref allPokemons, value); }
        }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            pokemonsRepository.GetAllPokemons();
        }
    }
}