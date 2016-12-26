using Dex.Core.DataAccess;
using Dex.Core.Entities;
using Dex.Core.Repositories;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dex.Core.Test.Repositories
{
    [TestFixture]
    public class PokemonRepositoryTests
    {
        private ushort[] Nothing = { };
        private IPokemonsDataSource pokemonDataSource;
        private PokemonBuilder PkBuilder => new PokemonBuilder();

        [Test]
        public async Task ShouldGetMaxAttackFromPokemonsList()
        {
            ushort maxAttack = 123;
            ushort lesserAttack = 34;

            Pokemon pokemon1 = PkBuilder.WithAttack(maxAttack);
            Pokemon pokemon2 = PkBuilder.WithAttack(lesserAttack);

            SetUpDataSourceMockData(new Pokemon[] { pokemon1, pokemon2 });

            PokemonRepository pokemonRepository = new PokemonRepository(pokemonDataSource);

            maxAttack.ShouldBe(await pokemonRepository.GetMaxBaseAttack());
        }

        [Test]
        public async Task ShouldGetMaxDefenseFromPokemonsList()
        {
            ushort maxDefense = 123;
            ushort lesserDefense = 34;

            Pokemon pokemon1 = PkBuilder.WithDefense(maxDefense);
            Pokemon pokemon2 = PkBuilder.WithDefense(lesserDefense);

            SetUpDataSourceMockData(new Pokemon[] { pokemon1, pokemon2 });

            PokemonRepository pokemonRepository = new PokemonRepository(pokemonDataSource);

            maxDefense.ShouldBe(await pokemonRepository.GetMaxBaseDefense());
        }

        [Test]
        public async Task ShouldGetMaxStaminaFromPokemonsList()
        {
            ushort maxStamina = 123;
            ushort lesserStamina = 34;

            Pokemon pokemon1 = PkBuilder.WithStamina(maxStamina);
            Pokemon pokemon2 = PkBuilder.WithStamina(lesserStamina);

            SetUpDataSourceMockData(new Pokemon[] { pokemon1, pokemon2 });

            PokemonRepository pokemonRepository = new PokemonRepository(pokemonDataSource);

            maxStamina.ShouldBe(await pokemonRepository.GetMaxBaseStamina());
        }

        private void SetUpDataSourceMockData(IEnumerable<Pokemon> list)
        {
            pokemonDataSource = Substitute.For<IPokemonsDataSource>();
            pokemonDataSource.LoadAllPokemonsAsync().Returns(Task.FromResult(list));
        }
    }
}