using Dex.Core.DataAccess;
using Dex.Core.Entities;
using Dex.Core.Services;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace Dex.Core.Test.Entities
{
    [TestFixture]
    public class TypeServiceTest
    {
        [TestCase(PokemonType.Electric, PokemonType.Flying, PokemonType.Water, 1.5625f)]
        public void GivenADoubleWeakPokemon_MultiplierShouldBeCorrect(
            PokemonType moveType,
            PokemonType defendingPokemonType1,
            PokemonType defendingPokemonType2,
            float expectedMultiplier)
        {
            var defendingPokemon = new Pokemon() { Types = new[] { defendingPokemonType1, defendingPokemonType2 } };
            var attackingMove = new QuickMove() { Type = moveType };

            var effectivenessSource = CreateEffectivenessSource();
            var typeService = new TypesService(effectivenessSource);

            typeService.GetTypeAdvantageMultiplier(attackingMove, defendingPokemon).Result.ShouldBe(expectedMultiplier);
        }

        private ITypeEffectivenessDataSource CreateEffectivenessSource()
        {
            var effectivenessExample = new Dictionary<PokemonType, TypeEffectiveness>()
            {
                [PokemonType.Electric] = new TypeEffectiveness(
                    new[] { PokemonType.Flying, PokemonType.Water },
                    Enumerable.Empty<PokemonType>()
                )
            };

            var dataSource = Substitute.For<ITypeEffectivenessDataSource>();
            dataSource.LoadTypeEffectivenessTable().Returns(effectivenessExample);

            return dataSource;
        }
    }
}