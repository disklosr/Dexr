using System.Collections.Generic;
using System.Diagnostics;

namespace Dex.Core.Entities
{
    [DebuggerDisplay("{Name}")]
    public class Pokemon
    {
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
        public ushort MaxCP { get; set; }

        #endregion GeneralStats

        #region Moves

        public IEnumerable<Move> QuickMoves { get; set; }
        public IEnumerable<Move> SpecialMoves { get; set; }

        #endregion Moves

        #region Other

        public ushort CandiesToEvolve { get; set; }
        public ushort EggDistance { get; set; }
        public string EvolvesFrom { get; set; }
        public string EvolvesTo { get; set; }

        #endregion Other
    }
}