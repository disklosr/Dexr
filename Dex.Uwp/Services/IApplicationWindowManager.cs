using Windows.UI;
using Windows.UI.Xaml;

namespace Dex.Uwp.Services
{
    public interface IApplicationWindowManager
    {
        void InitializeWindow();
    }

    public class ApplicationWindowManager : IApplicationWindowManager
    {
        public void InitializeWindow()
        {
            InitAppWindowColors();
        }

        private void InitAppWindowColors()
        {
            var resources = Application.Current.Resources;

            var titleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            titleBar.BackgroundColor = (Color)resources["SystemAccentColorDark1"];

            titleBar.InactiveBackgroundColor = (Color)resources["SystemAccentColor"];
            titleBar.ButtonBackgroundColor = (Color)resources["SystemAccentColorDark1"];
            titleBar.ButtonHoverBackgroundColor = (Color)resources["SystemAccentColorDark2"];
            titleBar.ButtonPressedBackgroundColor = (Color)resources["SystemAccentColorDark3"];
            titleBar.ButtonInactiveBackgroundColor = (Color)resources["SystemAccentColor"];
        }
    }
}