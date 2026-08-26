using System.Globalization;
using System.Windows;

namespace SwapiFrontEndWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        { }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Set current culture
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            CultureInfo currentUICulture = CultureInfo.CurrentUICulture;

            Thread.CurrentThread.CurrentCulture = currentCulture;
            Thread.CurrentThread.CurrentUICulture = currentUICulture;
        }
    }
}
