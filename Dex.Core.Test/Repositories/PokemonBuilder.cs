using Dex.Core.Entities;

namespace Dex.Core.Test.Repositories
{
    public class PokemonBuilder
    {
        private Pokemon _instance;

        public PokemonBuilder()
        {
            _instance = new Pokemon();
        }

        public static implicit operator Pokemon(PokemonBuilder instance)
        {
            return instance.Build();
        }

        public Pokemon Build()
        {
            return _instance;
        }

        public PokemonBuilder EvolvesFrom(ushort dexNumber)
        {
            _instance.EvolvesFrom = dexNumber;
            return this;
        }

        public PokemonBuilder EvolvesTo(ushort dexNumber)
        {
            _instance.EvolvesTo = new ushort[] { dexNumber };
            return this;
        }

        public PokemonBuilder EvolvesTo(ushort[] toEvolutions)
        {
            _instance.EvolvesTo = toEvolutions;
            return this;
        }

        public PokemonBuilder WithAttack(ushort attack)
        {
            _instance.Attack = new CombatStat(attack);
            return this;
        }

        public PokemonBuilder WithDefense(ushort defense)
        {
            _instance.Defense = new CombatStat(defense);
            return this;
        }

        public PokemonBuilder WithDexId(ushort dexNumber)
        {
            _instance.DexNumber = dexNumber;
            return this;
        }

        public PokemonBuilder WithStamina(ushort stamina)
        {
            _instance.Stamina = new CombatStat(stamina);
            return this;
        }
    }
}