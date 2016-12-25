namespace Dex.Core.Entities
{
    public abstract class Move
    {
        public ushort Attack { get; set; }
        public float CoolDown { get; set; }
        public string MoveId { get; set; }
        public MoveType MoveType { get; protected set; }
        public string Name { get; set; }
        public PokemonType Type { get; set; }
    }
}