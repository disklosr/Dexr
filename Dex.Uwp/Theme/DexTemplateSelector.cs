using Dex.Core.Entities;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Dex.Uwp.Theme
{
    public class MovesTemplateSelector : DataTemplateSelector
    {
        private Dictionary<System.Type, DataTemplate> TypeToTemplateMapper;

        public MovesTemplateSelector()
        {
            TypeToTemplateMapper = new Dictionary<System.Type, DataTemplate>()
            {
                [typeof(QuickMove)] = GetTemplateFromResourceKey("QuickMoveDetailsDataTemplate"),
                [typeof(ChargeMove)] = GetTemplateFromResourceKey("ChargeMoveDetailsDataTemplate"),
                [typeof(Pokemon)] = GetTemplateFromResourceKey("PokemonListItemTemplate")
            };
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (TypeToTemplateMapper.ContainsKey(item.GetType()))
                return TypeToTemplateMapper[item.GetType()];

            return base.SelectTemplateCore(item);
        }

        private DataTemplate GetTemplateFromResourceKey(string resourceKey)
        {
            return (DataTemplate)Application.Current.Resources[resourceKey];
        }
    }
}