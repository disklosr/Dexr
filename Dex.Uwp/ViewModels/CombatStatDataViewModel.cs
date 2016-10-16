using System;

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
        public ushort Percentage => (ushort)Math.Round((double)(100 * StatValue) / MaxStatValue);
        public ushort StatValue { get; }
    }
}