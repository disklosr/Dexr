using System.Diagnostics;

namespace Dex.Core.Entities
{
    [DebuggerDisplay("{Name}")]
    public class Pokemon
    {
        private CP _cp;

        #region General

        public ushort DexNumber { get; set; }

        public string Name { get; set; }

        public PokemonType Type1 { get; set; }

        public PokemonType Type2 { get; set; }

        #endregion General

        #region CombatStats

        public CombatStat Attack { get; set; }

        public CP Cp => _cp ?? (_cp = new CP(Attack, Defense, Stamina));

        public CombatStat Defense { get; set; }

        public CombatStat Stamina { get; set; }

        #endregion CombatStats

        #region GeneralStats

        public ushort CatchRate { get; set; }

        public ushort FleeRate { get; set; }

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
    }
}