namespace Dex.Uwp.ViewModels
{
    public class CombatStatDataViewModel
    {
        public CombatStatDataViewModel(ushort statValue, ushort maxStatValue)
        {
            MaxStatValue = maxStatValue;
            StatValue = statValue;
        }

        public ushort MaxStatValue { get; }
        public ushort StatValue { get; }
    }
}