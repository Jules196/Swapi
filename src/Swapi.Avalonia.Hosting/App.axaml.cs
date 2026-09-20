using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Swapi.Avalonia.Hosting.ViewModels;
using Swapi.Avalonia.Hosting.Views;

namespace Swapi.Avalonia.Hosting;

/// <summary>
/// Application class. The views and view models are provided by the host container.
/// </summary>
public partial class App : Application
{
    private readonly MainWindow? mainWindow;
    private readonly MainViewModel? viewModel;

    // Parameterless constructor for the XAML runtime loader and the designer.
    public App()
    {
    }

    public App(MainWindow mainWindow, MainViewModel viewModel)
    {
        this.mainWindow = mainWindow;
        this.viewModel = viewModel;
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
            && mainWindow is not null
            && viewModel is not null)
        {
            desktop.MainWindow = mainWindow;
            desktop.Exit += (_, _) => viewModel.Dispose();
            viewModel.LoadPeopleCommand.Execute(null);
        }

        base.OnFrameworkInitializationCompleted();
    }
}
