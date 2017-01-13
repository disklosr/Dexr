using Dex.Uwp.Services;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace Dex.Uwp.Pages
{
    public sealed partial class SidePane : UserControl
    {
        private Dictionary<string, Action> _menuItemToPage;
        private INavigationService _navigationService;
        private IStoreService _storeService;

        public SidePane()
        {
            InitializeComponent();
            InitMenuItemToPage();

            MyList.ItemsSource = _menuItemToPage.Keys;
            MyList.ItemClick += MyList_ItemClick;
            Loaded += SidePane_Loaded;
        }

        private void InitMenuItemToPage()
        {
            //TODO: Make these automaticly resolved to avoid adding new entries each time
            _menuItemToPage = new Dictionary<string, Action>()
            {
                ["Pokedex"] = () => _navigationService.NavigateToPokedexPage(),
                ["Movedex"] = () => _navigationService.NavigateToMovesPage(),
                ["Types"] = () => _navigationService.NavigateToTypesPage(),
                ["Settings"] = () => _navigationService.NavigateToSettingsPage(),
                ["About"] = () => _navigationService.NavigateToMoveAboutPage(),
                ["Rate This App"] = () => _storeService.RateThisApp()
            };
        }

        private void MyList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var currentPage = _navigationService.CurrentPage.Name.Replace("Page", string.Empty);
            if ((string)e.ClickedItem != currentPage)
                _menuItemToPage[(string)e.ClickedItem].Invoke();
        }

        private void SidePane_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            _storeService = ServiceLocator.Current.GetInstance<IStoreService>();
        }
    }
}