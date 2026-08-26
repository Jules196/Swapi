using System.Text.Json.Serialization;

namespace SwapiBackend.Models;

internal class PersonModel
{
    [JsonPropertyName("name")]
    public required string Name { get; set; } // Search field

    [JsonPropertyName("birth_year")]
    public string BirthYear { get; set; } = string.Empty;

    [JsonPropertyName("eye_color")]
    public string EyeColor { get; set; } = string.Empty;

    [JsonPropertyName("gender")]
    public string Gender { get; set; } = string.Empty;

    [JsonPropertyName("hair_color")]
    public string HairColor { get; set; } = string.Empty;

    [JsonPropertyName("height")]
    public string Height { get; set; } = string.Empty;

    [JsonPropertyName("mass")]
    public string Mass { get; set; } = string.Empty;

    [JsonPropertyName("skin_color")]
    public string SkinColor { get; set; } = string.Empty;

    [JsonPropertyName("homeworld")]
    public string HomeWorld { get; set; } = string.Empty;

    [JsonPropertyName("films")]
    public string[] Films { get; set; } = Array.Empty<string>();

    [JsonPropertyName("species")]
    public string[] Species { get; set; } = Array.Empty<string>();

    [JsonPropertyName("starships")]
    public string[] Starships { get; set; } = Array.Empty<string>();

    [JsonPropertyName("vehicles")]
    public string[] Vehicles { get; set; } = Array.Empty<string>();

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("edited")]
    public DateTime Edited { get; set; }
}
