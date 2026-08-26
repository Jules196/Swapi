using Swapi.Wpf.ViewModels;
using SwapiBackend;
using System.Windows;

namespace Swapi.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        MainViewModel viewModel = new(new StarWarsData());
        MainWindow mainWindow = new()
        {
            DataContext = viewModel,
        };

        MainWindow = mainWindow;
        mainWindow.Show();
        viewModel.LoadPeopleCommand.Execute(null);
    }
}
