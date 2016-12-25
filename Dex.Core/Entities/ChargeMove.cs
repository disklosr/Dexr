namespace Dex.Core.Entities
{
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
}