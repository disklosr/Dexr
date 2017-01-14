using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Dex.Uwp.IoC;
using Dex.Uwp.Pages;
using Dex.Uwp.Services;
using Microsoft.Practices.ServiceLocation;
using Serilog;

namespace Dex.Uwp
{
    sealed partial class App : Application
    {
        private IAppLifecycleManager _lifecycleManager;
        private ILogger _log;
        private INavigationService _navigationService;
        private Shell _rootShell;
        private ISettingsService _settingsService;

        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (_rootShell == null)
                await InitializeAppAsync(e);

            if (e.PrelaunchActivated)
                return;

            _rootShell = Window.Current.Content as Shell;

            if (_rootShell.Frame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter

                _navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
                _navigationService.NavigateToPokedexPage();
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        private async Task InitializeAppAsync(LaunchActivatedEventArgs e)
        {
            var ioc = new IocBootstrapper();
            _log = ServiceLocator.Current.GetInstance<ILogger>();
            _lifecycleManager = ServiceLocator.Current.GetInstance<IAppLifecycleManager>();

            if (await _lifecycleManager.IsFirstRun())
            {
                await _lifecycleManager.InitializeAppForFirstRun();
            }

            InitializeShell(e);
        }

        private void InitializeShell(LaunchActivatedEventArgs e)
        {
            _settingsService = ServiceLocator.Current.GetInstance<ISettingsService>();
            var appManager = new ApplicationWindowManager();
            appManager.SetAccentColor(_settingsService.AccentColor);
            appManager.InitializeWindow();

            _rootShell = new Shell();

            if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
                LoadPreviousState();
            }

            Window.Current.Content = _rootShell;

            _log.Debug("[Startup] Application Shell has been initialized.");
        }

        private void LoadPreviousState()
        {
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            _log.Debug("[Suspending] Application is being suspended.");
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}