using Dex.Uwp.IoC;
using Dex.Uwp.Pages;
using Dex.Uwp.Services;
using Microsoft.Practices.ServiceLocation;
using Serilog;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace Dex
{
    sealed partial class App : Application
    {
        private ILogger log;
        private INavigationService navigationService;
        private Shell rootShell;

        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            new ApplicationWindowManager().InitializeWindow();
            rootShell = Window.Current.Content as Shell;

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

        private void InitializeShell(LaunchActivatedEventArgs e)
        {
            var ioc = new IocBootstrapper();
            log = ServiceLocator.Current.GetInstance<ILogger>();

            rootShell = new Shell();

            if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
                LoadPreviousState();
            }

            Window.Current.Content = rootShell;

            log.Information("[Startup] Application Shell has been initialized.");
        }

        private void LoadPreviousState()
        {
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            log.Information("[Suspending] Application is being suspended.");
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}