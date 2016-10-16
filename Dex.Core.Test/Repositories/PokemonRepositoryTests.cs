using Dex.Core.DataAccess;
using Dex.Core.Entities;
using Dex.Core.Repositories;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dex.Core.Test.Repositories
{
    [TestFixture]
    public class PokemonRepositoryTests
    {
        private Mock<IPokemonsDataSource> mockPokemonsDataSource;

        [Test]
        public async Task Given1EvolutionsPokemonShouldReturnNothing()
        {
            var pokemon1 = new Pokemon() { Name = "poke1", DexNumber = 1, EvolvesFrom = null, EvolvesTo = null };
            var pokemon2 = new Pokemon() { Name = "poke2", DexNumber = 2, EvolvesFrom = null, EvolvesTo = null };
            var pokemon3 = new Pokemon() { Name = "poke3", DexNumber = 3, EvolvesFrom = null, EvolvesTo = null };
            var pokemon4 = new Pokemon() { Name = "poke4", DexNumber = 4, EvolvesFrom = null, EvolvesTo = null };

            SetUpDataSourceMockData(new Pokemon[] { pokemon1, pokemon2, pokemon3, pokemon4 });

            PokemonRepository pokemonRepository = new PokemonRepository(mockPokemonsDataSource.Object);
            var evolutionLine = await pokemonRepository.GetEvolutionLineFor(pokemon2);

            Assert.IsTrue(!evolutionLine.Any());
        }

        [Test]
        public async Task Given2EvolutionsPokemonShouldReturn2PokemonsInCorrectOrder()
        {
            var pokemon1 = new Pokemon() { Name = "poke1", DexNumber = 1, EvolvesFrom = null, EvolvesTo = "poke2" };
            var pokemon2 = new Pokemon() { Name = "poke2", DexNumber = 2, EvolvesFrom = "poke1", EvolvesTo = null };
            var pokemon3 = new Pokemon() { Name = "poke3", DexNumber = 3, EvolvesFrom = null, EvolvesTo = null };
            var pokemon4 = new Pokemon() { Name = "poke4", DexNumber = 4, EvolvesFrom = null, EvolvesTo = null };

            SetUpDataSourceMockData(new Pokemon[] { pokemon1, pokemon2, pokemon3, pokemon4 });

            PokemonRepository pokemonRepository = new PokemonRepository(mockPokemonsDataSource.Object);
            var evolutionLine = await pokemonRepository.GetEvolutionLineFor(pokemon2);

            Assert.IsTrue(evolutionLine.Count() == 2);
            CollectionAssert.AllItemsAreNotNull(evolutionLine);
            Assert.IsTrue(evolutionLine.ElementAt(0) == pokemon1);
            Assert.IsTrue(evolutionLine.ElementAt(1) == pokemon2);
        }

        [Test]
        public async Task Given3EvolutionsPokemonShouldReturn3PokemonsInCorrectOrder()
        {
            var pokemon1 = new Pokemon() { Name = "poke1", DexNumber = 1, EvolvesFrom = null, EvolvesTo = "poke2" };
            var pokemon2 = new Pokemon() { Name = "poke2", DexNumber = 2, EvolvesFrom = "poke1", EvolvesTo = "poke3" };
            var pokemon3 = new Pokemon() { Name = "poke3", DexNumber = 3, EvolvesFrom = "poke2", EvolvesTo = null };
            var pokemon4 = new Pokemon() { Name = "poke4", DexNumber = 4, EvolvesFrom = null, EvolvesTo = null };

            SetUpDataSourceMockData(new Pokemon[] { pokemon1, pokemon2, pokemon3, pokemon4 });

            PokemonRepository pokemonRepository = new PokemonRepository(mockPokemonsDataSource.Object);
            var evolutionLine = await pokemonRepository.GetEvolutionLineFor(pokemon2);

            Assert.IsTrue(evolutionLine.Count() == 3);
            CollectionAssert.AllItemsAreNotNull(evolutionLine);
            Assert.IsTrue(evolutionLine.ElementAt(0) == pokemon1);
            Assert.IsTrue(evolutionLine.ElementAt(1) == pokemon2);
            Assert.IsTrue(evolutionLine.ElementAt(2) == pokemon3);
        }

        [SetUp]
        public void SetUp()
        {
            mockPokemonsDataSource = new Mock<IPokemonsDataSource>();
        }

        [Test]
        public async Task ShouldGetMaxAttackFromPokemonsList()
        {
            ushort maxAttack = 123;
            ushort lesserAttack = 34;

            var pokemon1 = new Pokemon() { Attack = new Attack(maxAttack) };
            var pokemon2 = new Pokemon() { Attack = new Attack(lesserAttack) };

            SetUpDataSourceMockData(new Pokemon[] { pokemon1, pokemon2 });

            PokemonRepository pokemonRepository = new PokemonRepository(mockPokemonsDataSource.Object);

            Assert.AreEqual(maxAttack, await pokemonRepository.GetMaxAttack());
        }

        [Test]
        public async Task ShouldGetMaxDefenseFromPokemonsList()
        {
            ushort maxDefense = 123;
            ushort lesserDefense = 34;

            var pokemon1 = new Pokemon() { Defense = new Defense(maxDefense) };
            var pokemon2 = new Pokemon() { Defense = new Defense(lesserDefense) };

            SetUpDataSourceMockData(new Pokemon[] { pokemon1, pokemon2 });

            PokemonRepository pokemonRepository = new PokemonRepository(mockPokemonsDataSource.Object);

            Assert.AreEqual(maxDefense, await pokemonRepository.GetMaxDefense());
        }

        [Test]
        public async Task ShouldGetMaxStaminaFromPokemonsList()
        {
            ushort maxStamina = 123;
            ushort lesserStamina = 34;

            var pokemon1 = new Pokemon() { Stamina = new Stamina(maxStamina) };
            var pokemon2 = new Pokemon() { Stamina = new Stamina(lesserStamina) };

            SetUpDataSourceMockData(new Pokemon[] { pokemon1, pokemon2 });

            PokemonRepository pokemonRepository = new PokemonRepository(mockPokemonsDataSource.Object);

            Assert.AreEqual(maxStamina, await pokemonRepository.GetMaxStamina());
        }

        private void SetUpDataSourceMockData(IEnumerable<Pokemon> list)
        {
            mockPokemonsDataSource
                .Setup(x => x.LoadAllPokemonsAsync())
                .Returns(Task.FromResult(list));
        }
    }
}