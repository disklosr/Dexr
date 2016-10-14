using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Type = Dex.Core.Entities.Type;

namespace Dex.Uwp.ValueConverters
{
    public class PokemonTypeToColorConverter : IValueConverter
    {
        private readonly Dictionary<ushort, SolidColorBrush> TypeToColorMap;

        public PokemonTypeToColorConverter()
        {
            TypeToColorMap = new Dictionary<ushort, SolidColorBrush>()
            {
                {(ushort)Type.Bug,      GetSolidColorBrush("#a8b820") },
                {(ushort)Type.Grass,    GetSolidColorBrush("#78c850") },
                {(ushort)Type.Dark,     GetSolidColorBrush("#705848") },
                {(ushort)Type.Ground,   GetSolidColorBrush("#e0c068") },
                {(ushort)Type.Dragon,   GetSolidColorBrush("#7038f8") },
                {(ushort)Type.Ice,      GetSolidColorBrush("#98d8d8") },
                {(ushort)Type.Electric, GetSolidColorBrush("#f8d030") },
                {(ushort)Type.Normal,   GetSolidColorBrush("#8a8a59") },
                {(ushort)Type.Fairy,    GetSolidColorBrush("#e898e8") },
                {(ushort)Type.Poison,   GetSolidColorBrush("#a040a0") },
                {(ushort)Type.Fighting, GetSolidColorBrush("#c03028") },
                {(ushort)Type.Psychic,  GetSolidColorBrush("#f85888") },
                {(ushort)Type.Fire,     GetSolidColorBrush("#f08030") },
                {(ushort)Type.Rock,     GetSolidColorBrush("#b8a038") },
                {(ushort)Type.Flying,   GetSolidColorBrush("#a890f0") },
                {(ushort)Type.Steel,    GetSolidColorBrush("#b8b8d0") },
                {(ushort)Type.Ghost,    GetSolidColorBrush("#705898") },
                {(ushort)Type.Water,    GetSolidColorBrush("#6890f0") },
                {(ushort)Type.Unknown,  GetSolidColorBrush("#8a8a59")  }
            };
        }

        public object Convert(object value, System.Type targetType, object parameter, string language)
        {
            Type type = (Type)value;
            return TypeToColorMap[(ushort)type];
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        private SolidColorBrush GetSolidColorBrush(string hex)
        {
            hex = hex.Replace("#", string.Empty);
            byte r = (byte)(System.Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte g = (byte)(System.Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte b = (byte)(System.Convert.ToUInt32(hex.Substring(4, 2), 16));

            return new SolidColorBrush(Color.FromArgb(0xFF, r, g, b));
        }
    }
}