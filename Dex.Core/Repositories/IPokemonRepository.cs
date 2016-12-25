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

        Task<IEnumerable<Pokemon>> GetEvolutionLineFor(Pokemon pokemon);

        Task<ushort> GetMaxBaseAttack();

        Task<ushort> GetMaxBaseDefense();

        Task<ushort> GetMaxBaseStamina();

        Task<Pokemon> GetNextPokemon(ushort PokemonId);

        Task<Pokemon> GetPokemonById(ushort pokemonId);

        Task<IEnumerable<Pokemon>> GetPokemonsWithMove(string moveId);

        Task<Pokemon> GetPreviousPokemon(ushort PokemonId);

        bool HasNextPokemon(ushort PokemonId);

        bool HasPreviousPokemon(ushort PokemonId);
    }

    public class PokemonRepository : IPokemonRepository
    {
        private readonly IPokemonsDataSource dataSource;

        private IEnumerable<Pokemon> allPokemonsCache;

        private CombatStat maxAttack;
        private CombatStat maxDefense;
        private ushort maxDexNumber;
        private CombatStat maxStamina;
        private ushort minDexNumber = 1;

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

        public async Task<ushort> GetMaxBaseAttack()
        {
            await EnsureCacheIsValid();
            maxAttack = maxAttack ?? allPokemonsCache.Max(poke => poke.Attack);
            return maxAttack.Value;
        }

        public async Task<ushort> GetMaxBaseDefense()
        {
            await EnsureCacheIsValid();
            maxDefense = maxDefense ?? allPokemonsCache.Max(poke => poke.Defense);
            return maxDefense.Value;
        }

        public async Task<ushort> GetMaxBaseStamina()
        {
            await EnsureCacheIsValid();
            maxStamina = maxStamina ?? allPokemonsCache.Max(poke => poke.Stamina);
            return maxStamina.Value;
        }

        public async Task<Pokemon> GetNextPokemon(ushort PokemonId)
        {
            await EnsureCacheIsValid();
            return await GetPokemonById((ushort)(PokemonId + 1));
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

        public async Task<IEnumerable<Pokemon>> GetPokemonsWithMove(string moveId)
        {
            await EnsureCacheIsValid();
            return allPokemonsCache.Where(poke => poke.Moves.ChargeMovesIds.Contains(moveId) || poke.Moves.QuickMovesIds.Contains(moveId));
        }

        public async Task<Pokemon> GetPreviousPokemon(ushort PokemonId)
        {
            await EnsureCacheIsValid();
            return await GetPokemonById((ushort)(PokemonId - 1));
        }

        public bool HasNextPokemon(ushort PokemonId)
        {
            return PokemonId < maxDexNumber;
        }

        public bool HasPreviousPokemon(ushort PokemonId)
        {
            return PokemonId > minDexNumber;
        }

        private async Task EnsureCacheIsValid()
        {
            if (allPokemonsCache == null)
            {
                allPokemonsCache = await dataSource.LoadAllPokemonsAsync();
                maxDexNumber = allPokemonsCache.Max(poke => poke.DexNumber);
            }
        }

        private async Task<IEnumerable<Pokemon>> GetEvolutions(Pokemon pokemon)
        {
            var from = await GetPokemonById(pokemon.EvolvesFrom);

            var to = await Task.WhenAll(
                pokemon.EvolvesTo.ToList()
                .Select(async pokemonId => await GetPokemonById(pokemonId)));

            var evolutions = new List<Pokemon>(to.ToList());
            evolutions.Add(from);

            return evolutions.Where(poke => poke != null);
        }

        private bool HasEvolutions(Pokemon pokemon)
        {
            return pokemon.EvolvesFrom != 0 || pokemon.EvolvesTo.Count() != 0;
        }
    }
}