using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Swapi.Avalonia.Hosting.ViewModels;
using Swapi.Avalonia.Hosting.Views;

namespace Swapi.Avalonia.Hosting;

/// <summary>
/// Application class. The views and view models are provided by the host
/// container, so this class only connects them to the Avalonia lifetime.
/// </summary>
public partial class App : Application
{
    /// <summary>Main window created by the dependency injection container; <see langword="null"/> in the designer.</summary>
    private readonly MainWindow? mainWindow;

    /// <summary>View model of <see cref="mainWindow"/>; <see langword="null"/> in the designer.</summary>
    private readonly MainViewModel? viewModel;

    /// <summary>
    /// Parameterless constructor required by the XAML runtime loader and the
    /// previewer. Instances created this way have no injected dependencies and
    /// therefore do not show a window.
    /// </summary>
    public App()
    {
    }

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
    /// Loads App.axaml, which contains the theme and the application resources.
    /// </summary>
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    /// <summary>
    /// Attaches the injected window to the desktop lifetime and starts the
    /// initial data load once the framework is ready.
    /// </summary>
    public override void OnFrameworkInitializationCompleted()
    {
        // The null checks cover the designer/runtime-loader case where the
        // parameterless constructor was used.
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
            && mainWindow is not null
            && viewModel is not null)
        {
            desktop.MainWindow = mainWindow;

            // Dispose the view model (and the backend service) on shutdown.
            desktop.Exit += (_, _) => viewModel.Dispose();

            // Fire and forget: the command runs asynchronously and reports its
            // state through the view model properties.
            viewModel.LoadPeopleCommand.Execute(null);
        }

        base.OnFrameworkInitializationCompleted();
    }
}
