﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Dex.Core.Entities
{
    [DebuggerDisplay("{Name}")]
    public class Pokemon
    {
        private int maxCP;

        #region General

        public ushort DexNumber { get; set; }
        public string Name { get; set; }
        public Type Type1 { get; set; }
        public Type Type2 { get; set; }

        #endregion General

        #region CombatStats

        public Attack Attack { get; set; }
        public Defense Defense { get; set; }
        public Stamina Stamina { get; set; }

        #endregion CombatStats

        #region GeneralStats

        public ushort CatchRate { get; set; }
        public ushort FleeRate { get; set; }

        public int MaxCP
        {
            get
            {
                if (maxCP == 0)
                    maxCP = GetMaxCp();
                return maxCP;
            }
        }

        private int GetMaxCp()
        {
            var calculatedCp = Math.Floor((Attack.Value + 15) * Math.Pow(Defense.Value + 15, 0.5) * Math.Pow(Stamina.Value + 15, 0.5) * Math.Pow(LevelToCPM[40], 2) / 10);
            return Math.Max((int)10, (int)calculatedCp);
        }

        #endregion GeneralStats

        #region Moves

        public PokemonMovesIds Moves { get; set; }

        #endregion Moves

        #region Other

        public ushort CandiesToEvolve { get; set; }
        public ushort EggDistance { get; set; }
        public ushort EvolvesFrom { get; set; }
        public ushort[] EvolvesTo { get; set; }

        #endregion Other

        #region CPMultipliers

        //https://www.reddit.com/r/TheSilphRoad/comments/50w3lx/precise_cp_multiplier_values_for_each_level_help/
        private Dictionary<float, double> LevelToCPM = new Dictionary<float, double>()
        {
            [1.0f] = 0.09399999678134918,
            [1.5f] = 0.13513743132352830,
            [2.0f] = 0.16639786958694458,
            [2.5f] = 0.19265091419219970,
            [3.0f] = 0.21573247015476227,
            [3.5f] = 0.23657265305519104,
            [4.0f] = 0.25572004914283750,
            [4.5f] = 0.27353037893772125,
            [5.0f] = 0.29024988412857056,
            [5.5f] = 0.30605737864971160,
            [6.0f] = 0.32108759880065920,
            [6.5f] = 0.33544503152370453,
            [7.0f] = 0.34921267628669740,
            [7.5f] = 0.36245773732662200,
            [8.0f] = 0.37523558735847473,
            [8.5f] = 0.38759241108516856,
            [9.0f] = 0.39956727623939514,
            [9.5f] = 0.41119354951725060,
            [10.0f] = 0.4225000143051148,
            [10.5f] = 0.4329264134104144,
            [11.0f] = 0.4431075453758240,
            [11.5f] = 0.4530599538719858,
            [12.0f] = 0.4627983868122100,
            [12.5f] = 0.4723360780626535,
            [13.0f] = 0.4816849529743195,
            [13.5f] = 0.4908558102324605,
            [14.0f] = 0.4998584389686584,
            [14.5f] = 0.5087017565965652,
            [15.0f] = 0.5173939466476440,
            [15.5f] = 0.5259425118565559,
            [16.0f] = 0.5343543291091919,
            [16.5f] = 0.5426357612013817,
            [17.0f] = 0.5507926940917969,
            [17.5f] = 0.5588305993005633,
            [18.0f] = 0.5667545199394226,
            [18.5f] = 0.5745691470801830,
            [19.0f] = 0.5822789072990417,
            [19.5f] = 0.5898879119195044,
            [20.0f] = 0.5974000096321106,
            [20.5f] = 0.6048236563801765,
            [21.0f] = 0.6121572852134705,
            [21.5f] = 0.6194041110575199,
            [22.0f] = 0.6265671253204346,
            [22.5f] = 0.6336491815745830,
            [23.0f] = 0.6406529545783997,
            [23.5f] = 0.6475809663534164,
            [24.0f] = 0.6544356346130370,
            [24.5f] = 0.6612192690372467,
            [25.0f] = 0.6679340004920960,
            [25.5f] = 0.6745819002389908,
            [26.0f] = 0.6811649203300476,
            [26.5f] = 0.6876849085092545,
            [27.0f] = 0.6941436529159546,
            [27.5f] = 0.7005428969860077,
            [28.0f] = 0.7068842053413391,
            [28.5f] = 0.7131690979003906,
            [29.0f] = 0.7193990945816040,
            [29.5f] = 0.7255756109952927,
            [30.0f] = 0.7317000031471252,
            [30.5f] = 0.7347410172224045,
            [31.0f] = 0.7377694845199585,
            [31.5f] = 0.7407855764031410,
            [32.0f] = 0.74378943,
            [32.5f] = 0.746781204,
            [33.0f] = 0.74976104,
            [33.5f] = 0.752729104,
            [34.0f] = 0.75568551,
            [34.5f] = 0.75863037,
            [35.0f] = 0.76156384,
            [35.5f] = 0.76448607,
            [36.0f] = 0.76739717,
            [36.5f] = 0.77029727,
            [37.0f] = 0.7731865,
            [37.5f] = 0.77606494,
            [38.0f] = 0.77893275,
            [38.5f] = 0.78179006,
            [39.0f] = 0.78463697,
            [39.5f] = 0.78747358,
            [40.0f] = 0.79030001,
            [40.5f] = 1
        };

        #endregion CPMultipliers
    }
}