namespace Swapi.Avalonia.Hosting;

/// <summary>
/// Options bound from the <c>Swapi</c> configuration section.
/// </summary>
public sealed class SwapiOptions
{
    public const string SectionName = "Swapi";

    /// <summary>
    /// Optional source URI of the Star Wars API. An empty value uses the default endpoint.
    /// </summary>
    public string SourceUri { get; set; } = "";
}
