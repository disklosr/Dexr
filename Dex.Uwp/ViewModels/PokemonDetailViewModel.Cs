using Dex.Core.Entities;
using Dex.Core.Repositories;
using Dex.Uwp.Infrastructure;
using Dex.Uwp.Services;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace Dex.Uwp.ViewModels
{
    public class PokemonDetailViewModel : ViewModelBase
    {
        private readonly IMoveRepository moveRepository;
        private readonly INavigationService navigationService;
        private readonly IPokemonRepository pokemonRepository;

        private CombatStatDataViewModel attackStats;
        private CombatStatDataViewModel defenseStats;
        private EvolutionLineDataViewModel evolutionLine;
        private PokemonMoves moves;
        private Pokemon selectedPokemon;
        private CombatStatDataViewModel staminaStats;

        public PokemonDetailViewModel(IPokemonRepository pokemonRepository, IMoveRepository moveRepository, INavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.moveRepository = moveRepository;
            this.pokemonRepository = pokemonRepository;

            PreviousCommand = new RelayCommand(async () => await SetNewPokemon(await pokemonRepository.GetPreviousPokemon(SelectedPokemon.DexNumber)), () => pokemonRepository.HasPreviousPokemon(SelectedPokemon.DexNumber));
            NextCommand = new RelayCommand(async () => await SetNewPokemon(await pokemonRepository.GetNextPokemon(SelectedPokemon.DexNumber)), () => pokemonRepository.HasNextPokemon(SelectedPokemon.DexNumber));
        }

        public CombatStatDataViewModel AttackStats
        {
            get { return attackStats; }
            private set { Set(ref attackStats, value); }
        }

        public CombatStatDataViewModel DefenseStats
        {
            get { return defenseStats; }
            private set { Set(ref defenseStats, value); }
        }

        public EvolutionLineDataViewModel EvolutionLine
        {
            get { return evolutionLine; }
            private set { Set(ref evolutionLine, value); }
        }

        public PokemonMoves Moves
        {
            get { return moves; }
            private set { Set(ref moves, value); }
        }

        public RelayCommand NextCommand { get; }

        public RelayCommand PreviousCommand { get; }

        public Pokemon SelectedPokemon
        {
            get { return selectedPokemon; }
            private set { Set(ref selectedPokemon, value); }
        }

        public CombatStatDataViewModel StaminaStats
        {
            get { return staminaStats; }
            private set { Set(ref staminaStats, value); }
        }

        public void OnMoveSelected(Move move)
        {
            navigationService.ResolveFromThenNavigate(move);
        }

        public async override Task OnNavigatedTo(NavigationEventArgs e)
        {
            var dexId = (ushort)e.Parameter;
            await SetNewPokemon(await pokemonRepository.GetPokemonById(dexId));
        }

        private async Task InitCombatStats()
        {
            var maxAtk = pokemonRepository.GetMaxBaseAttack();
            var maxDef = pokemonRepository.GetMaxBaseDefense();
            var maxStm = pokemonRepository.GetMaxBaseStamina();
            await Task.WhenAll(new Task[] { maxAtk, maxDef, maxStm });

            AttackStats = new CombatStatDataViewModel(SelectedPokemon.Attack.Value, maxAtk.Result);
            DefenseStats = new CombatStatDataViewModel(SelectedPokemon.Defense.Value, maxDef.Result);
            StaminaStats = new CombatStatDataViewModel(SelectedPokemon.Stamina.Value, maxStm.Result);
        }

        private async Task SetNewPokemon(Pokemon newPokemon)
        {
            SelectedPokemon = newPokemon;
            Moves = await moveRepository.GetMovesById(SelectedPokemon.Moves.QuickMovesIds, SelectedPokemon.Moves.ChargeMovesIds);
            EvolutionLine = new EvolutionLineDataViewModel(await pokemonRepository.GetEvolutionLineFor(selectedPokemon));
            await InitCombatStats();
            NextCommand.RaiseCanExecuteChanged();
            PreviousCommand.RaiseCanExecuteChanged();
        }
    }
}