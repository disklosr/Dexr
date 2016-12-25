using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using PokemonType = Dex.Core.Entities.PokemonType;

namespace Dex.Uwp.ValueConverters
{
    public class PokemonTypeToColorConverter : IValueConverter
    {
        private readonly Dictionary<ushort, SolidColorBrush> TypeToColorMap;

        public PokemonTypeToColorConverter()
        {
            TypeToColorMap = new Dictionary<ushort, SolidColorBrush>()
            {
                {(ushort)PokemonType.Bug,      GetSolidColorBrush("#a8b820") },
                {(ushort)PokemonType.Grass,    GetSolidColorBrush("#78c850") },
                {(ushort)PokemonType.Dark,     GetSolidColorBrush("#705848") },
                {(ushort)PokemonType.Ground,   GetSolidColorBrush("#e0c068") },
                {(ushort)PokemonType.Dragon,   GetSolidColorBrush("#7038f8") },
                {(ushort)PokemonType.Ice,      GetSolidColorBrush("#98d8d8") },
                {(ushort)PokemonType.Electric, GetSolidColorBrush("#f8d030") },
                {(ushort)PokemonType.Normal,   GetSolidColorBrush("#8a8a59") },
                {(ushort)PokemonType.Fairy,    GetSolidColorBrush("#e898e8") },
                {(ushort)PokemonType.Poison,   GetSolidColorBrush("#a040a0") },
                {(ushort)PokemonType.Fighting, GetSolidColorBrush("#c03028") },
                {(ushort)PokemonType.Psychic,  GetSolidColorBrush("#f85888") },
                {(ushort)PokemonType.Fire,     GetSolidColorBrush("#f08030") },
                {(ushort)PokemonType.Rock,     GetSolidColorBrush("#b8a038") },
                {(ushort)PokemonType.Flying,   GetSolidColorBrush("#a890f0") },
                {(ushort)PokemonType.Steel,    GetSolidColorBrush("#b8b8d0") },
                {(ushort)PokemonType.Ghost,    GetSolidColorBrush("#705898") },
                {(ushort)PokemonType.Water,    GetSolidColorBrush("#6890f0") },
                {(ushort)PokemonType.Unknown,  GetSolidColorBrush("#8a8a59")  }
            };
        }

        public object Convert(object value, System.Type targetType, object parameter, string language)
        {
            PokemonType type = (PokemonType)value;
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