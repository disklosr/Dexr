﻿using Dex.Core.DataAccess;
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

        Task<ushort> GetMaxAttack();

        Task<ushort> GetMaxDefense();

        Task<ushort> GetMaxStamina();

        Task<Pokemon> GetNextPokemon(ushort PokemonId);

        Task<Pokemon> GetPokemonById(ushort pokemonId);

        Task<Pokemon> GetPreviousPokemon(ushort PokemonId);

        bool HasNextPokemon(ushort PokemonId);

        bool HasPreviousPokemon(ushort PokemonId);
    }

    public class PokemonRepository : IPokemonRepository
    {
        private readonly IPokemonsDataSource dataSource;

        private IEnumerable<Pokemon> allPokemonsCache;

        private Attack maxAttack;
        private Defense maxDefense;
        private ushort maxDexNumber;
        private Stamina maxStamina;

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
            var to = await GetPokemonById(pokemon.EvolvesTo);
            var from = await GetPokemonById(pokemon.EvolvesFrom);

            return new Pokemon[] { to, from }.Where(poke => poke != null);
        }

        private bool HasEvolutions(Pokemon pokemon)
        {
            return pokemon.EvolvesFrom != 0 || pokemon.EvolvesTo != 0;
        }
    }
}