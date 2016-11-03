using Dex.Uwp.Infrastructure;
using Dex.Uwp.Services;
using Microsoft.Practices.ServiceLocation;
using Windows.UI.Xaml.Navigation;

namespace Dex.Uwp.Pages
{
    public sealed partial class SearchPage : PageBase
    {
        public SearchPage()
        {
            InitializeComponent();
            Loaded += SearchPage_Loaded;
            KeyUp += SearchPage_KeyUp;
        }

        private void SearchPage_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Escape)
            {
                var navService = ServiceLocator.Current.GetInstance<INavigationService>();
                navService.GoBack();
            }
        }

        private void SearchPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            SearchBox.Focus(Windows.UI.Xaml.FocusState.Programmatic);
            Loaded -= SearchPage_Loaded;
        }
    }
}