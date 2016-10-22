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

        private CombatStatDataViewModel attackStats;
        private CombatStatDataViewModel defenseStats;
        private EvolutionLineDataViewModel evolutionLine;
        private Pokemon selectedPokemon;
        private CombatStatDataViewModel staminaStats;

        public PokemonDetailViewModel(IPokemonRepository pokemonRepository)
        {
            this.pokemonRepository = pokemonRepository;
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

        public async override Task OnNavigatedTo(NavigationEventArgs e)
        {
            var dexId = (ushort)e.Parameter;
            SelectedPokemon = await pokemonRepository.GetPokemonById(dexId);
            EvolutionLine = new EvolutionLineDataViewModel(await pokemonRepository.GetEvolutionLineFor(selectedPokemon));
            await InitCombatStats();
        }

        private async Task InitCombatStats()
        {
            var maxAtk = pokemonRepository.GetMaxAttack();
            var maxDef = pokemonRepository.GetMaxDefense();
            var maxStm = pokemonRepository.GetMaxStamina();
            await Task.WhenAll(new Task[] { maxAtk, maxDef, maxStm });

            AttackStats = new CombatStatDataViewModel(SelectedPokemon.Attack.Value, maxAtk.Result);
            DefenseStats = new CombatStatDataViewModel(SelectedPokemon.Defense.Value, maxDef.Result);
            StaminaStats = new CombatStatDataViewModel(SelectedPokemon.Stamina.Value, maxStm.Result);
        }
    }
}