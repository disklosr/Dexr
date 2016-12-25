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

        private ushort[] Nothing = { };
        private PokemonBuilder PkBuilder => new PokemonBuilder();

        [Test]
        public async Task Given1EvolutionsPokemonShouldReturnNothing()
        {
            Pokemon pokemon1 = PkBuilder.WithDexId(1).EvolvesFrom(0).EvolvesTo(Nothing);
            Pokemon pokemon2 = PkBuilder.WithDexId(2).EvolvesFrom(0).EvolvesTo(Nothing);
            Pokemon pokemon3 = PkBuilder.WithDexId(3).EvolvesFrom(0).EvolvesTo(Nothing);
            Pokemon pokemon4 = PkBuilder.WithDexId(4).EvolvesFrom(0).EvolvesTo(Nothing);

            SetUpDataSourceMockData(new Pokemon[] { pokemon1, pokemon2, pokemon3, pokemon4 });

            PokemonRepository pokemonRepository = new PokemonRepository(mockPokemonsDataSource.Object);
            var evolutionLine = await pokemonRepository.GetEvolutionLineFor(pokemon2);

            Assert.IsTrue(!evolutionLine.Any());
        }

        [Test]
        public async Task Given2EvolutionsPokemonShouldReturn2PokemonsInCorrectOrder()
        {
            Pokemon pokemon1 = PkBuilder.WithDexId(1).EvolvesFrom(0).EvolvesTo(1);
            Pokemon pokemon2 = PkBuilder.WithDexId(2).EvolvesFrom(1).EvolvesTo(Nothing);
            Pokemon pokemon3 = PkBuilder.WithDexId(3).EvolvesFrom(2).EvolvesTo(Nothing);
            Pokemon pokemon4 = PkBuilder.WithDexId(4).EvolvesFrom(0).EvolvesTo(Nothing);

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
            Pokemon pokemon1 = PkBuilder.WithDexId(1).EvolvesFrom(0).EvolvesTo(2);
            Pokemon pokemon2 = PkBuilder.WithDexId(2).EvolvesFrom(1).EvolvesTo(3);
            Pokemon pokemon3 = PkBuilder.WithDexId(3).EvolvesFrom(2).EvolvesTo(Nothing);
            Pokemon pokemon4 = PkBuilder.WithDexId(4).EvolvesFrom(0).EvolvesTo(Nothing);

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

            Pokemon pokemon1 = PkBuilder.WithAttack(maxAttack);
            Pokemon pokemon2 = PkBuilder.WithAttack(lesserAttack);

            SetUpDataSourceMockData(new Pokemon[] { pokemon1, pokemon2 });

            PokemonRepository pokemonRepository = new PokemonRepository(mockPokemonsDataSource.Object);

            Assert.AreEqual(maxAttack, await pokemonRepository.GetMaxBaseAttack());
        }

        [Test]
        public async Task ShouldGetMaxDefenseFromPokemonsList()
        {
            ushort maxDefense = 123;
            ushort lesserDefense = 34;

            Pokemon pokemon1 = PkBuilder.WithDefense(maxDefense);
            Pokemon pokemon2 = PkBuilder.WithDefense(lesserDefense);

            SetUpDataSourceMockData(new Pokemon[] { pokemon1, pokemon2 });

            PokemonRepository pokemonRepository = new PokemonRepository(mockPokemonsDataSource.Object);

            Assert.AreEqual(maxDefense, await pokemonRepository.GetMaxBaseDefense());
        }

        [Test]
        public async Task ShouldGetMaxStaminaFromPokemonsList()
        {
            ushort maxStamina = 123;
            ushort lesserStamina = 34;

            Pokemon pokemon1 = PkBuilder.WithStamina(maxStamina);
            Pokemon pokemon2 = PkBuilder.WithStamina(lesserStamina);

            SetUpDataSourceMockData(new Pokemon[] { pokemon1, pokemon2 });

            PokemonRepository pokemonRepository = new PokemonRepository(mockPokemonsDataSource.Object);

            Assert.AreEqual(maxStamina, await pokemonRepository.GetMaxBaseStamina());
        }

        private void SetUpDataSourceMockData(IEnumerable<Pokemon> list)
        {
            mockPokemonsDataSource
                .Setup(x => x.LoadAllPokemonsAsync())
                .Returns(Task.FromResult(list));
        }
    }
}