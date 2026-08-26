using System.Text.Json.Serialization;

namespace SwapiBackend.Models;

internal class AllPersonsModel
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("next")]
    public string? Next { get; set; }

    [JsonPropertyName("previous")]
    public string? Previous { get; set; }

    [JsonPropertyName("results")]
    public List<PersonModel>? Persons { get; set; }
}
