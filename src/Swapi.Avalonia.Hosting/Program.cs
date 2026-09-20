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
internal static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        using IHost host = CreateHost(args);

        host.Services.GetRequiredService<ILogger<App>>()
            .LogInformation("Starting the Avalonia host sample.");

        BuildAvaloniaApp(host.Services).StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp() => BuildAvaloniaApp(CreateHost([]).Services);

    private static AppBuilder BuildAvaloniaApp(IServiceProvider services)
        => AppBuilder.Configure(services.GetRequiredService<App>)
            .UsePlatformDetect()
#if DEBUG
            .WithDeveloperTools()
#endif
            .WithInterFont()
            .LogToTrace();

    private static IHost CreateHost(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        builder.Services.Configure<SwapiOptions>(builder.Configuration.GetSection(SwapiOptions.SectionName));
        builder.Services.AddSingleton<ISwapiPersons>(serviceProvider =>
            new StarWarsData(serviceProvider.GetRequiredService<IOptions<SwapiOptions>>().Value.SourceUri));
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<MainWindow>();
        builder.Services.AddSingleton<App>();

        return builder.Build();
    }
}
