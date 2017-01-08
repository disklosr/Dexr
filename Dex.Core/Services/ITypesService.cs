using Dex.Core.DataAccess;
using Dex.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dex.Core.Services
{
    public interface ITypesService
    {
    }

    public class TypesService : ITypesService
    {
        private const float NeutralMultiplier = 1f;
        private const float StrongMultiplier = 1.25f;
        private const float WeakMultiplier = 0.8f;

        private readonly ITypeEffectivenessDataSource _typesDataSource;
        private Dictionary<PokemonType, TypeEffectiveness> _typesEffectivenessMap;

        public TypesService(ITypeEffectivenessDataSource typesDataSource)
        {
            _typesDataSource = typesDataSource;
        }

        public async Task<float> GetTypeAdvantageMultiplier(Move attackingMove, Pokemon defendingPokemon)
        {
            await EnsureDataWasInitialized();

            var damageMultiplier = NeutralMultiplier;

            foreach (var defendingType in defendingPokemon.Types)
            {
                damageMultiplier *= await GetEffectiveness(attackingMove.Type, defendingType);
            }

            return damageMultiplier;
        }

        private async Task EnsureDataWasInitialized()
        {
            if (_typesEffectivenessMap == null)
                _typesEffectivenessMap = await _typesDataSource.LoadTypeEffectivenessTable();
        }

        private async Task<float> GetEffectiveness(PokemonType attacker, PokemonType defender)
        {
            await EnsureDataWasInitialized();

            if (_typesEffectivenessMap[attacker].StrongAgainst.Contains(defender))
                return StrongMultiplier;

            if (_typesEffectivenessMap[attacker].WeakAgainst.Contains(defender))
                return WeakMultiplier;

            return NeutralMultiplier;
        }
    }
}