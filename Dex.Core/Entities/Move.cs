namespace Dex.Core.Entities
{
    public class Move
    {
        public double CoolDown { get; set; }
        public double DamagePerSecond { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }
    }
}