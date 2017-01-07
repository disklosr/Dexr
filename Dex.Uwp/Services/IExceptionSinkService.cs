using Serilog;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace Dex.Uwp.Services
{
    public interface IExceptionSinkService
    {
    }

    public class UwpExceptionSinkService : IExceptionSinkService
    {
        private readonly ILogger _log;

        private UwpExceptionSinkService(ILogger log)
        {
            _log = log;
        }

        public static void CreateExceptionSink(ILogger log)
        {
            var exceptionSink = new UwpExceptionSinkService(log);
            exceptionSink.Initialize();
        }

        private void DisplayMessageDialog()
        {
            MessageDialog messageDialog = new MessageDialog(
                "Dex has encountered an application error. I'm sorry for the inconvenience.",
                "Well, This is embarrassing."
            );
            messageDialog.Commands.Add(new UICommand("Ok") { Id = 0 });
            messageDialog.ShowAsync();
        }

        private void HandleException(Exception e)
        {
            LogException(e.GetType().Name, e.Message);
            DisplayMessageDialog();
        }

        private void Initialize()
        {
            Application.Current.UnhandledException += OnUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
        }

        private void LogException(string exceptionType, string message)
        {
            _log.Fatal("[Unhandled] (Exception: {Type}, Message: {Message}).", exceptionType, message);
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            HandleException(e.Exception);
        }

        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            HandleException(e.Exception);
        }
    }
}