using System.Text.Json;
using SwapiBackend.Models;

namespace SwapiBackend.Mapping;

/// <summary>
/// Json mapping extension methods.
/// </summary>
internal static class JsonMapping
{
    /// <summary>
    /// Converts a <paramref name="jsonString"/> string to a <see cref="PersonModel"/>.
    /// </summary>
    /// <param name="jsonString">Raw json string.</param>
    /// <returns>If it could not create a <see cref="PersonModel"/>, it returns <see langword="null"/> instead of <see cref="PersonModel"/>.</returns>
    internal static PersonModel? ToPersonModel(this string jsonString)
    {
        if (string.IsNullOrWhiteSpace(jsonString)) return null;

        try
        {
            return JsonSerializer.Deserialize<PersonModel>(jsonString);
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// Converts a <paramref name="jsonString"/> string to a <see cref="AllPersonsModel"/>.
    /// </summary>
    /// <param name="jsonString">Raw json string.</param>
    /// <returns>If it could not create a <see cref="AllPersonsModel"/>, it returns <see langword="null"/> instead of <see cref="AllPersonsModel"/>.</returns>
    internal static AllPersonsModel? ToAllPersonModels(this string jsonString)
    {
        if (string.IsNullOrWhiteSpace(jsonString)) return null;

        try
        {
            return JsonSerializer.Deserialize<AllPersonsModel>(jsonString);
        }
        catch (Exception)
        {
            return null;
        }
    }
}
