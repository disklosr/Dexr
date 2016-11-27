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
        private Dictionary<string, Action> MenuItemToPage;
        private INavigationService navigationService;

        public SidePane()
        {
            InitializeComponent();
            InitMenuItemToPage();

            MyList.ItemsSource = MenuItemToPage.Keys;
            MyList.ItemClick += MyList_ItemClick;
            Loaded += SidePane_Loaded;
        }

        private void InitMenuItemToPage()
        {
            //TODO: Make these automaticly resolved to avoid adding new entries each time
            MenuItemToPage = new Dictionary<string, Action>()
            {
                ["Pokedex"] = () => navigationService.NavigateToPokedexPage(),
                ["Movedex"] = () => navigationService.NavigateToMovesPage(),
                ["Settings"] = () => navigationService.NavigateToSettingsPage(),
                ["About"] = () => navigationService.NavigateToMoveAboutPage()
            };
        }

        private void MyList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var currentPage = navigationService.CurrentPage.Name.Replace("Page", string.Empty);
            if ((string)e.ClickedItem != currentPage)
                MenuItemToPage[(string)e.ClickedItem].Invoke();
        }

        private void SidePane_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
        }
    }
}