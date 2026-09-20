using Swapi.Wpf.Hosting.ViewModels;
using System.Windows;

namespace Swapi.Wpf.Hosting;

/// <summary>
/// Interaction logic for App.xaml. All dependencies are resolved by the host.
/// </summary>
public partial class App : Application
{
    private readonly MainWindow mainWindow;
    private readonly MainViewModel viewModel;

    public App(MainWindow mainWindow, MainViewModel viewModel)
    {
        this.mainWindow = mainWindow;
        this.viewModel = viewModel;
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        MainWindow = mainWindow;
        mainWindow.Show();
        viewModel.LoadPeopleCommand.Execute(null);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        viewModel.Dispose();
        base.OnExit(e);
    }
}
