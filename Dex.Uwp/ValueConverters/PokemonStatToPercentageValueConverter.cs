using Dex.Uwp.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Dex.Uwp.ValueConverters
{
    public class PokemonStatToPercentageValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var columnNumber = (string)parameter;
            var stats = (CombatStatDataViewModel)value;

            if (value == null)
                return columnNumber == "0"
                    ? new GridLength(0)
                    : new GridLength(1, GridUnitType.Star);

            int percent = (int)Math.Round((double)(100 * stats.StatValue) / stats.MaxStatValue);

            return columnNumber == "0"
                ? new GridLength(percent, GridUnitType.Star)
                : new GridLength(100 - percent, GridUnitType.Star);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}