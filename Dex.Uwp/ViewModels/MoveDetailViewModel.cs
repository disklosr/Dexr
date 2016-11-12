﻿using Dex.Core.Entities;
using Dex.Core.Repositories;
using Dex.Uwp.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace Dex.Uwp.ViewModels
{
    public class MoveDetailViewModel : ViewModelBase
    {
        private readonly IMoveRepository moveRepository;
        private readonly IPokemonRepository pokemonRepository;

        private IEnumerable<Pokemon> relatedPokemons;
        private Move selectedMove;

        public MoveDetailViewModel(IPokemonRepository pokemonRepository, IMoveRepository moveRepository)
        {
            this.moveRepository = moveRepository;
            this.pokemonRepository = pokemonRepository;
        }

        public IEnumerable<Pokemon> RelatedPokemons
        {
            get { return relatedPokemons; }
            private set { Set(ref relatedPokemons, value); }
        }

        public Move SelectedMove
        {
            get { return selectedMove; }
            private set { Set(ref selectedMove, value); }
        }

        public async override Task OnNavigatedTo(NavigationEventArgs e)
        {
            var moveId = (string)e.Parameter;
            await SetNewMove(moveRepository.GetMoveById(moveId));
        }

        private async Task SetNewMove(Move move)
        {
            SelectedMove = move;
            RelatedPokemons = await pokemonRepository.GetPokemonsWithMove(move.MoveId);
        }
    }
}