using Dex.Uwp.Pages;
using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Dex.Uwp.Services
{
    public interface INavigationService
    {
        void Navigate(Type pageType, string parameter = null, bool serializeParam = true);

        void NavigateToPokedexPage();

        void NavigateToPokemonDetailsPage(ushort dexNumber);
    }

    public class NavigationService : INavigationService
    {
        private Frame mainFrame;

        public NavigationService()
        {
            EnsureNavigationFrameIsAvailable();
            InitBackButton();

            mainFrame.NavigationFailed += MainFrame_NavigationFailed;
            mainFrame.Navigating += MainFrame_Navigating;
            mainFrame.Navigated += MainFrame_Navigated;
        }

        public void Navigate(Type pageType, string parameter = null, bool serializeParam = true)
        {
            EnsureNavigationFrameIsAvailable();

            if (parameter == null)
                mainFrame.Navigate(pageType);
            else
            {
                if (serializeParam)
                {
                    //TODO: Serialize param
                }

                mainFrame.Navigate(pageType, parameter);
            }
        }

        public void NavigateToPokedexPage()
        {
            mainFrame.Navigate(typeof(PokedexPage));
        }

        public void NavigateToPokemonDetailsPage(ushort dexNumber)
        {
            mainFrame.Navigate(typeof(PokemonDetailPage), dexNumber);
        }

        private void EnsureNavigationFrameIsAvailable()
        {
            var content = Window.Current.Content;

            // The default state is that we expect to have the
            // AppShell as a hosting view for content
            if (content is Shell)
            {
                var appShell = content as Shell;
                mainFrame = appShell.Frame;
            }

            // We can also have a simple frame when the user
            // chooses to use the share target contract to share
            // photos from the Windows photos app.
            else if (content is Frame)
            {
                var frameShell = content as Frame;
                mainFrame = frameShell;
            }
            else
            {
                throw new NavigationServiceException($"Could not find navigation frame within app window.");
            }
        }

        private void InitBackButton()
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += (s, e) =>
            {
                EnsureNavigationFrameIsAvailable();
                if (mainFrame.CanGoBack)
                {
                    e.Handled = true;
                    mainFrame.GoBack();
                }
            };
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