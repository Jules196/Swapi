using Swapi.Wpf.Hosting.ViewModels;
using System.Windows;

namespace Swapi.Wpf.Hosting;

/// <summary>
/// Interaction logic for App.xaml. All dependencies are resolved by the host,
/// so this class only wires the injected window to the application lifetime.
/// </summary>
public partial class App : Application
{
    /// <summary>Main window created by the dependency injection container.</summary>
    private readonly MainWindow mainWindow;

    /// <summary>View model of <see cref="mainWindow"/>; kept to trigger the initial load and to dispose it.</summary>
    private readonly MainViewModel viewModel;

    /// <summary>
    /// Creates the application with the services resolved by the host container.
    /// </summary>
    /// <param name="mainWindow">The main window of the application.</param>
    /// <param name="viewModel">The view model bound to the main window.</param>
    public App(MainWindow mainWindow, MainViewModel viewModel)
    {
        this.mainWindow = mainWindow;
        this.viewModel = viewModel;
    }

    /// <summary>
    /// Shows the injected main window and starts the initial data load.
    /// </summary>
    /// <param name="e">Startup arguments provided by WPF.</param>
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Assigning MainWindow makes WPF shut down the application when the window closes.
        MainWindow = mainWindow;
        mainWindow.Show();

        // Fire and forget: the command runs asynchronously and reports its state
        // through the view model properties.
        viewModel.LoadPeopleCommand.Execute(null);
    }

    /// <summary>
    /// Releases the view model (and with it the backend service) on shutdown.
    /// </summary>
    /// <param name="e">Exit arguments provided by WPF.</param>
    protected override void OnExit(ExitEventArgs e)
    {
        viewModel.Dispose();
        base.OnExit(e);
    }
}
