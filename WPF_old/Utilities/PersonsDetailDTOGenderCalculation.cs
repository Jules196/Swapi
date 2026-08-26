using SwapiBackend.DTOs;

namespace SwapiFrontEndWPF.Utilities;

/// <summary>
/// Gender calculation extensions.
/// </summary>
internal static class PersonsDetailDTOGenderCalculation
{
    /// <summary>
    /// Calculates male and female counts of <paramref name="persons"/>.
    /// </summary>
    /// <param name="persons"></param>
    /// <returns>(double male count, double female count).</returns>
    internal static (double, double) CountMalesAndFemales(this IEnumerable<PersonDetailDTO> persons)
    {
        double maleCounter = 0;
        double femaleCounter = 0;

        foreach (PersonDetailDTO person in persons)
        {
            if (string.IsNullOrWhiteSpace(person.Gender)) continue;
            else if (string.Equals(person.Gender, "male", StringComparison.OrdinalIgnoreCase))
            {
                maleCounter++;
            }
            else if (string.Equals(person.Gender, "female", StringComparison.OrdinalIgnoreCase))
            {
                femaleCounter++;
            }
        }
        return (maleCounter, femaleCounter);
    }
}
