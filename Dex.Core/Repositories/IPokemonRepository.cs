using Dex.Core.DataAccess;
using Dex.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dex.Core.Repositories
{
    public interface IPokemonRepository
    {
        Task<IEnumerable<Pokemon>> GetAllPokemons();

        Task<IEnumerable<Pokemon>> GetEvolutionLineFor(Pokemon pokemon);

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

        public async Task<IEnumerable<Pokemon>> GetEvolutionLineFor(Pokemon pokemon)
        {
            if (!HasEvolutions(pokemon))
                return Enumerable.Empty<Pokemon>();

            HashSet<Pokemon> evolutions = new HashSet<Pokemon>();

            var level1Search = await GetEvolutions(pokemon);

            foreach (var poke in level1Search)
            {
                evolutions.Add(poke);
                var level2Search = await GetEvolutions(poke);
                foreach (var p in level2Search)
                {
                    evolutions.Add(p);
                }
            }

            evolutions.Add(pokemon);
            return evolutions.OrderBy(poke => poke.DexNumber);
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
                .FirstOrDefault();
        }

        public async Task<Pokemon> GetPokemonByName(string pokemonName)
        {
            await EnsureCacheIsValid();
            return allPokemonsCache
                .Where(pokemon => pokemon.Name == pokemonName)
                .FirstOrDefault();
        }

        private async Task EnsureCacheIsValid()
        {
            if (allPokemonsCache == null)
                allPokemonsCache = await dataSource.LoadAllPokemonsAsync();
        }

        private async Task<IEnumerable<Pokemon>> GetEvolutions(Pokemon pokemon)
        {
            var to = await GetPokemonByName(pokemon.EvolvesTo);
            var from = await GetPokemonByName(pokemon.EvolvesFrom);

            return new Pokemon[] { to, from }.Where(poke => poke != null);
        }

        private bool HasEvolutions(Pokemon pokemon)
        {
            return pokemon.EvolvesFrom != null || pokemon.EvolvesTo != null;
        }
    }
}