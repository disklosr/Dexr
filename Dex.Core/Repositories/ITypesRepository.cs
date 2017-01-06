using Dex.Core.Entities;

namespace Dex.Core.Repositories
{
    public interface ITypesRepository
    {
        float GetDamageMultiplier(PokemonType attackingMoveType, PokemonType defendingPokemonType);
    }
}