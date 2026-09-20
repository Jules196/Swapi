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
public static class Program
{
    [STAThread]
    public static int Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        builder.Services.Configure<SwapiOptions>(builder.Configuration.GetSection(SwapiOptions.SectionName));
        builder.Services.AddSingleton<ISwapiPersons>(serviceProvider =>
            new StarWarsData(serviceProvider.GetRequiredService<IOptions<SwapiOptions>>().Value.SourceUri));
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<MainWindow>();
        builder.Services.AddSingleton<App>();

        using IHost host = builder.Build();

        ILogger<App> logger = host.Services.GetRequiredService<ILogger<App>>();
        logger.LogInformation("Starting the WPF host sample.");

        App app = host.Services.GetRequiredService<App>();
        app.InitializeComponent();

        return app.Run();
    }
}
