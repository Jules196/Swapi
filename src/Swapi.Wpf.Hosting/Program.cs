using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swapi.Wpf.Hosting.ViewModels;
using SwapiBackend;
using System.Windows;

namespace Swapi.Wpf.Hosting;

/// <summary>
/// Entry point of the sample. The application is composed with the
/// <see cref="HostApplicationBuilder"/> so that configuration, logging and
/// dependency injection are available before any WPF type is created.
/// </summary>
/// <remarks>
/// Compared to the plain <c>Swapi.Wpf</c> sample the <c>App</c> class is no
/// longer the startup object. The project file points the <c>StartupObject</c>
/// to this class, which builds the host first and only then starts WPF.
/// </remarks>
public static class Program
{
    /// <summary>
    /// Builds the host, resolves the WPF application and runs the message loop.
    /// </summary>
    /// <param name="args">Command line arguments; they are also used as a configuration source.</param>
    /// <returns>The exit code returned by <see cref="Application.Run()"/>.</returns>
    [STAThread] // WPF requires a single-threaded apartment for the UI thread.
    public static int Main(string[] args)
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

        // Building the host creates the service provider. The host is disposed
        // after Run returns, which also disposes all singleton services.
        using IHost host = builder.Build();

        ILogger<App> logger = host.Services.GetRequiredService<ILogger<App>>();
        logger.LogInformation("Starting the WPF host sample.");

        // Resolving App injects the already created window and view model.
        App app = host.Services.GetRequiredService<App>();

        // InitializeComponent loads App.xaml (resources, converters). It must be
        // called explicitly because the application is not the startup object.
        app.InitializeComponent();

        // Run starts the WPF message loop and blocks until the application exits.
        return app.Run();
    }
}
