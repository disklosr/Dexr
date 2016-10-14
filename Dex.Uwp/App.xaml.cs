using Dex.Uwp.IoC;
using Dex.Uwp.Pages;
using Dex.Uwp.Services;
using Microsoft.Practices.ServiceLocation;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Xaml;

namespace Dex
{
    sealed partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }

        private Shell rootShell;
        private INavigationService navigationService;

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            rootShell = Window.Current.Content as Shell;
            InitAppWindow();

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootShell == null)
                InitializeShell(e);

            if (e.PrelaunchActivated == false)
            {
                if (rootShell.Frame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter

                    navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
                    navigationService.NavigateToPokedexPage();
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        private void InitAppWindow()
        {
            var titleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            titleBar.BackgroundColor = (Color)Resources["SystemAccentColorDark1"];

            titleBar.InactiveBackgroundColor = (Color)Resources["SystemAccentColor"];
            titleBar.ButtonBackgroundColor = (Color)Resources["SystemAccentColorDark1"];
            titleBar.ButtonHoverBackgroundColor = (Color)Resources["SystemAccentColorDark2"];
            titleBar.ButtonPressedBackgroundColor = (Color)Resources["SystemAccentColorDark3"];
            titleBar.ButtonInactiveBackgroundColor = (Color)Resources["SystemAccentColor"];
        }

        private void InitializeShell(LaunchActivatedEventArgs e)
        {
            rootShell = new Shell();
            var ioc = new IocBootstrapper();

            if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
                LoadPreviousState();
            }

            Window.Current.Content = rootShell;
        }

        private void LoadPreviousState()
        {
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}