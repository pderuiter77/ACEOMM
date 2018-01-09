using NLog;
using System.Windows;
using System.Windows.Threading;

namespace ACEOMM
{
    public partial class App : Application
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            logger.Fatal(e.Exception);
            var innerException = e.Exception;
            while (innerException.InnerException != null)
                innerException = innerException.InnerException;
            MessageBox.Show(innerException.Message, "Unexpected exception", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
