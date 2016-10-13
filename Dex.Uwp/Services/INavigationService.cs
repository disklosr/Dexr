using Dex.Uwp.Pages;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Dex.Uwp.Services
{
    public interface INavigationService
    {
        void NavigateToPokedexPage();
    }

    public class NavigationService : INavigationService
    {
        private readonly Frame mainFrame;

        public NavigationService(Frame mainFrame)
        {
            this.mainFrame = mainFrame;
            mainFrame.NavigationFailed += MainFrame_NavigationFailed;
            mainFrame.Navigating += MainFrame_Navigating;
            mainFrame.Navigated += MainFrame_Navigated;
        }

        public void NavigateToPokedexPage()
        {
            mainFrame.Navigate(typeof(PokedexPage));
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
        }

        private void MainFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
        }

        private void MainFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
        }
    }
}