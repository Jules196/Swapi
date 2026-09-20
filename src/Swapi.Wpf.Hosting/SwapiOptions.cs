namespace Swapi.Wpf.Hosting;

/// <summary>
/// Options bound from the <c>Swapi</c> configuration section of
/// <c>appsettings.json</c> (or any other configuration source of the host).
/// </summary>
public sealed class SwapiOptions
{
    /// <summary>
    /// Name of the configuration section these options are read from.
    /// </summary>
    public const string SectionName = "Swapi";

    /// <summary>
    /// Optional source URI of the Star Wars API. An empty value makes the
    /// backend fall back to its default endpoint.
    /// </summary>
    public string SourceUri { get; set; } = "";
}
