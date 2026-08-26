using System.ComponentModel.DataAnnotations;

namespace SwapiBackend.DTOs;

/// <summary>
/// DTO of possible updates for a person.
/// </summary>
/// <param name="Name"></param>
/// <param name="Height"></param>
/// <param name="BirthYear"></param>
/// <param name="Gender"></param>
public record PersonUpdateDTO(
    [Required] string Name,
    string Height,
    string BirthYear,
    string Gender)
{ }
