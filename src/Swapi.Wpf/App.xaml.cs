using Swapi.Wpf.ViewModels;
using SwapiBackend;
using System.Windows;

namespace Swapi.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private MainViewModel? viewModel;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        viewModel = new MainViewModel(new StarWarsData());
        MainWindow mainWindow = new()
        {
            DataContext = viewModel,
        };

        MainWindow = mainWindow;
        mainWindow.Show();
        viewModel.LoadPeopleCommand.Execute(null);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        viewModel?.Dispose();
        base.OnExit(e);
    }
}
