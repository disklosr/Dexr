using Dex.Uwp.DataAccess;
using Microsoft.Practices.ServiceLocation;
using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace Dex.Uwp.ValueConverters
{
    public class PokemonDexNumberToPictureConverter : IValueConverter
    {
        private IPokePicturesSourceProvider _sourceProvider;
        private IPokePicturesSource _picturesSource;

        public PokemonDexNumberToPictureConverter()
        {
            _sourceProvider = ServiceLocator.Current.GetInstance<IPokePicturesSourceProvider>();
            _picturesSource = _sourceProvider.Source;
            _sourceProvider.SourceChanged += () => _picturesSource = _sourceProvider.Source;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var dexNumber = (ushort)value;
            return new BitmapImage(new Uri(_picturesSource.GetPath(dexNumber)));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}