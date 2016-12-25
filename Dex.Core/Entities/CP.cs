using Dex.Core.Extensions;
using System;

using CpMap = Dex.Core.Entities.CpMultipliersMap;

namespace Dex.Core.Entities
{
    public class CP
    {
        private CombatStat _attack;
        private CombatStat _defense;
        private CombatStat _stamina;

        public CP(CombatStat attack, CombatStat defense, CombatStat stamina)
        {
            if (attack == null)
                throw new ArgumentNullException(nameof(attack));

            if (defense == null)
                throw new ArgumentNullException(nameof(defense));

            if (stamina == null)
                throw new ArgumentNullException(nameof(stamina));

            _attack = attack;
            _defense = defense;
            _stamina = stamina;
        }

        public ushort Max => Calculate(_attack.Min, _defense.Min, _stamina.Min, 40.5f);

        public ushort Min => Calculate(_attack.Min, _defense.Min, _stamina.Min, 1.0f);

        public ushort AtLevel(float level)
        {
            return Calculate(_attack.Value, _defense.Value, _stamina.Value, level);
        }

        private static ushort Calculate(ushort attack, ushort defense, ushort stamina, float pokemonLevel)
        {
            if (!CpMap.LevelToCpm.ContainsKey(pokemonLevel))
                throw new ArgumentException($"Invalid Pokemon level ({pokemonLevel})");

            var calculatedCp = (ushort)Math.Floor(
                  attack
                * defense.Pow(0.5)
                * stamina.Pow(0.5)
                * CpMap.LevelToCpm[pokemonLevel].Pow(2)
                / 10);

            return calculatedCp.ClipToMin(10);
        }
    }
}