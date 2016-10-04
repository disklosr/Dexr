using Dex.Core.Entities;
using System.Collections.Generic;

namespace Dex.Core.Repositories
{
    public interface IPokemonRepository
    {
        IEnumerable<Pokemon> GetAllPokemons();

        Pokemon GetPokemonById(ushort pokemonId);
    }
}