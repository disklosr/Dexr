using Dex.Uwp.Services;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace Dex.Uwp.Pages
{
    public sealed partial class SidePane : UserControl
    {
        private Dictionary<string, Type> MenuItemToPage = new Dictionary<string, Type>()
        {
            ["Pokedex"] = typeof(PokedexPage),
            ["Movedex"] = typeof(MovedexPage)
        };

        private INavigationService navigationService;
        private string selectedMenuItem;

        public SidePane()
        {
            this.InitializeComponent();
            MyList.ItemsSource = MenuItemToPage.Keys;
            selectedMenuItem = MenuItemToPage.First().Key;
            this.Loaded += SidePane_Loaded;
        }

        private string SelectedMenuItem
        {
            get
            {
                return selectedMenuItem;
            }

            set
            {
                if (value != null && value != selectedMenuItem)
                {
                    selectedMenuItem = value;
                    navigationService.Navigate(MenuItemToPage[selectedMenuItem]);
                }
            }
        }

        private void SidePane_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
        }
    }
}