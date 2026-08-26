using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Swapi.Avalonia.ViewModels;
using Swapi.Avalonia.Views;
using SwapiBackend;

namespace Swapi.Avalonia;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            MainViewModel viewModel = new(new StarWarsData());
            desktop.MainWindow = new MainWindow
            {
                DataContext = viewModel,
            };
            viewModel.LoadPeopleCommand.Execute(null);
        }

        base.OnFrameworkInitializationCompleted();
    }
}