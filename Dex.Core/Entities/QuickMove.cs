namespace Dex.Core.Entities
{
    public class QuickMove : Move
    {
        public QuickMove()
        {
            MoveType = MoveType.Quick;
        }

        public ushort Energy { get; set; }
    }
}