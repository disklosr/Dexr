using Dex.Core.Entities;

namespace Dex.Scaper.DTO
{
    public class MoveDTO
    {
        public ushort Attack { get; set; }
        public ushort Charges { get; set; }
        public float CoolDown { get; set; }
        public float Critical { get; set; }
        public float Dodge { get; set; }
        public ushort Energy { get; set; }
        public MoveType MoveType { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }
    }
}