namespace Dex.Core.Entities
{
    public enum MoveType
    {
        Quick,
        Charge
    }

    public class ChargeMove : Move
    {
        public ChargeMove()
        {
            MoveType = MoveType.Charge;
        }

        public ushort Charges { get; set; }
        public float Critical { get; set; }
        public float Dodge { get; set; }
    }

    public abstract class Move
    {
        public ushort Attack { get; set; }
        public float CoolDown { get; set; }
        public string MoveId { get; set; }
        public MoveType MoveType { get; protected set; }
        public string Name { get; set; }
        public Type Type { get; set; }
    }

    public class QuickMove : Move
    {
        public QuickMove()
        {
            MoveType = MoveType.Quick;
        }

        public ushort Energy { get; set; }
    }
}