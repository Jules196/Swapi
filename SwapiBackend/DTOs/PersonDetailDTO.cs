using System.ComponentModel.DataAnnotations;

namespace SwapiBackend.DTOs;

/// <summary>
/// DTO for the details of a person.
/// </summary>
/// <param name="Name"></param>
/// <param name="Height"></param>
/// <param name="Mass"></param>
/// <param name="BirthYear"></param>
/// <param name="Gender"></param>
public record PersonDetailDTO(
    [Required] string Name,
    string Height="",
    string Mass="",
    string BirthYear="",
    string Gender="")
{ }
