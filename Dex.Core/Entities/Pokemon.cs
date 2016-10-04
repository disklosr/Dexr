using System.Collections.Generic;
using System.Linq;

namespace Dex.Core.Entities
{
    public sealed class MissingNo : Pokemon
    {
        public MissingNo()
        {
            Id = ushort.MaxValue;
            Name = "MissingNo";
            Type = Type.Unknown;

            Attack = new Attack(0);
            Defense = new Defense(0);
            Stamina = new Stamina(0);

            QuickMoves = Enumerable.Empty<Move>();
            SpecialMoves = Enumerable.Empty<Move>();
        }
    }

    public class Pokemon
    {
        #region General

        public ushort Id { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }

        #endregion General

        #region Stats

        public Attack Attack { get; set; }
        public Defense Defense { get; set; }
        public Stamina Stamina { get; set; }

        #endregion Stats

        #region Moves

        public IEnumerable<Move> QuickMoves { get; set; }
        public IEnumerable<Move> SpecialMoves { get; set; }

        #endregion Moves
    }
}