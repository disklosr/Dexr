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

        Task<ushort> GetMaxAttack();

        Task<ushort> GetMaxDefense();

        Task<ushort> GetMaxStamina();

        Task<Pokemon> GetPokemonById(ushort pokemonId);
    }

    public class PokemonRepository : IPokemonRepository
    {
        private readonly IPokemonsDataSource dataSource;

        private IEnumerable<Pokemon> allPokemonsCache;

        private Attack maxAttack;
        private Defense maxDefense;
        private Stamina maxStamina;

        public PokemonRepository(IPokemonsDataSource dataSource)
        {
            this.dataSource = dataSource;
        }

        public async Task<IEnumerable<Pokemon>> GetAllPokemons()
        {
            await EnsureCacheIsValid();
            return allPokemonsCache;
        }

        public async Task<ushort> GetMaxAttack()
        {
            await EnsureCacheIsValid();
            maxAttack = maxAttack ?? allPokemonsCache.Max(poke => poke.Attack);
            return maxAttack.Value;
        }

        public async Task<ushort> GetMaxDefense()
        {
            await EnsureCacheIsValid();
            maxDefense = maxDefense ?? allPokemonsCache.Max(poke => poke.Defense);
            return maxDefense.Value;
        }

        public async Task<ushort> GetMaxStamina()
        {
            await EnsureCacheIsValid();
            maxStamina = maxStamina ?? allPokemonsCache.Max(poke => poke.Stamina);
            return maxStamina.Value;
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