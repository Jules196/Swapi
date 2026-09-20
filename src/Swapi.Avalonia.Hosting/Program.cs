using Avalonia;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swapi.Avalonia.Hosting.ViewModels;
using Swapi.Avalonia.Hosting.Views;
using SwapiBackend;
using System;

namespace Swapi.Avalonia.Hosting;

/// <summary>
/// Entry point of the sample. The application is composed with the
/// <see cref="HostApplicationBuilder"/> so that configuration, logging and
/// dependency injection are available before Avalonia is initialized.
/// </summary>
/// <remarks>
/// Avalonia keeps its own <see cref="AppBuilder"/>; the generic host only
/// provides the services. Both builders are combined by handing the factory of
/// the <see cref="App"/> instance to <see cref="AppBuilder.Configure{TApp}(Func{TApp})"/>.
/// </remarks>
internal static class Program
{
    /// <summary>
    /// Builds the host and starts the classic desktop lifetime of Avalonia.
    /// </summary>
    /// <param name="args">Command line arguments; they are also used as a configuration source.</param>
    /// <remarks>
    /// Do not use any Avalonia, third-party API or SynchronizationContext-reliant
    /// code before the Avalonia application is started: things are not initialized yet.
    /// </remarks>
    [STAThread] // Required by the Windows backend for the UI thread.
    public static void Main(string[] args)
    {
        // Disposing the host at the end also disposes all singleton services.
        using IHost host = CreateHost(args);

        host.Services.GetRequiredService<ILogger<App>>()
            .LogInformation("Starting the Avalonia host sample.");

        // Blocks until the last window is closed.
        BuildAvaloniaApp(host.Services).StartWithClassicDesktopLifetime(args);
    }

    /// <summary>
    /// Avalonia configuration, don't remove; also used by the visual designer.
    /// </summary>
    /// <returns>A configured <see cref="AppBuilder"/> backed by its own host instance.</returns>
    /// <remarks>
    /// The designer calls this parameterless overload, therefore it creates a
    /// separate host that is only used to resolve the application instance.
    /// </remarks>
    public static AppBuilder BuildAvaloniaApp() => BuildAvaloniaApp(CreateHost([]).Services);

    /// <summary>
    /// Creates the <see cref="AppBuilder"/> and lets the container provide the
    /// <see cref="App"/> instance together with its dependencies.
    /// </summary>
    /// <param name="services">Service provider of the generic host.</param>
    /// <returns>The configured Avalonia application builder.</returns>
    private static AppBuilder BuildAvaloniaApp(IServiceProvider services)
        => AppBuilder.Configure(services.GetRequiredService<App>)
            .UsePlatformDetect() // Selects the windowing/rendering backend of the current OS.
#if DEBUG
            .WithDeveloperTools() // Enables the diagnostics overlay in debug builds only.
#endif
            .WithInterFont()
            .LogToTrace();

    /// <summary>
    /// Registers all services of the sample and builds the generic host.
    /// </summary>
    /// <param name="args">Command line arguments used as an additional configuration source.</param>
    /// <returns>The built host; the caller is responsible for disposing it.</returns>
    private static IHost CreateHost(string[] args)
    {
        // Host.CreateApplicationBuilder sets up configuration (appsettings.json,
        // environment variables, command line), logging and the service container.
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        // Bind the "Swapi" section of appsettings.json to the strongly typed options class.
        builder.Services.Configure<SwapiOptions>(builder.Configuration.GetSection(SwapiOptions.SectionName));

        // The backend service is a singleton because it caches the downloaded data.
        // Its source URI comes from the bound configuration options.
        builder.Services.AddSingleton<ISwapiPersons>(serviceProvider =>
            new StarWarsData(serviceProvider.GetRequiredService<IOptions<SwapiOptions>>().Value.SourceUri));

        // View model, window and application are singletons as well: the sample
        // shows exactly one window and its state must survive the whole session.
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<MainWindow>();
        builder.Services.AddSingleton<App>();

        return builder.Build();
    }
}
