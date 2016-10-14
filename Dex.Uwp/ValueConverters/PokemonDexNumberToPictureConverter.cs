using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace Dex.Uwp.ValueConverters
{
    public class PokemonDexNumberToPictureConverter : IValueConverter
    {
        private const string PokemonPicturePathFormat = "ms-appx:///Assets/Pokemons/{0}.png";

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var dexNumber = (ushort)value;
            return new BitmapImage(new Uri(string.Format(PokemonPicturePathFormat, dexNumber.ToString("D3"))));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}