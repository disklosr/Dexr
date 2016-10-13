using Dex.Core.DataAccess;
using Dex.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dex.Core.Repositories
{
    public interface IPokemonRepository
    {
        Task<IEnumerable<Pokemon>> GetAllPokemons();

        Task<Pokemon> GetPokemonById(ushort pokemonId);
    }

    public class PokemonRepository : IPokemonRepository
    {
        private readonly IPokemonsDataSource dataSource;

        private IEnumerable<Pokemon> allPokemonsCache;

        public PokemonRepository(IPokemonsDataSource dataSource)
        {
            this.dataSource = dataSource;
        }

        public async Task<IEnumerable<Pokemon>> GetAllPokemons()
        {
            await EnsureCacheIsValid();
            return allPokemonsCache;
        }

        public async Task<Pokemon> GetPokemonById(ushort pokemonId)
        {
            await EnsureCacheIsValid();
            return allPokemonsCache
                .Where(pokemon => pokemon.DexNumber == pokemonId)
                .DefaultIfEmpty(new MissingNo()).First();
        }

        private async Task EnsureCacheIsValid()
        {
            if (allPokemonsCache == null)
                allPokemonsCache = await dataSource.LoadAllPokemonsAsync();
        }
    }
}