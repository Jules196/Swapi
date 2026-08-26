using System.ComponentModel.DataAnnotations;

namespace SwapiBackend.DTOs;

/// <summary>
/// Lightweighted DTO for a person.
/// </summary>
/// <param name="Name"></param>
public record PersonDTO([Required] string Name);
