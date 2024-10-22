using SwapiBackend.DTOs;

namespace SwapiFrontEndWPF.Utilities;

/// <summary>
/// Age calculation extensions.
/// </summary>
internal static class PersonsDetailDTOAgeCalculation
{
    /// <summary>
    /// Normalize the string year to relative year.
    /// </summary>
    /// <param name="year"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Is fired if the format is not correct.</exception>
    internal static double NormalizedYearToBattleOfYavin(this string year)
    {
        if (string.IsNullOrWhiteSpace(year)) throw new ArgumentException("Invalid year format");

        else if (year.ToLower().EndsWith("bby"))
        {
            if (double.TryParse(year.AsSpan(0, year.Length - 3), out double result))
            {
                return -result;
            }
        }
        else if (year.ToLower().EndsWith("aby"))
        {
            if (double.TryParse(year.AsSpan(0, year.Length - 3), out double result))
            {
                return result;
            }
        }
        throw new ArgumentException("Invalid year format");
    }

    /// <summary>
    /// Calculate a age relative to <paramref name="relativeYear"/>.
    /// </summary>
    /// <param name="birthYear"></param>
    /// <param name="relativeYear">Relative year.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Is fired if the format is not correct.</exception>
    internal static double CalculateAge(this string birthYear, string relativeYear)
    {
        if (string.IsNullOrWhiteSpace(birthYear)) throw new ArgumentException("Invalid year format");
        if (string.IsNullOrWhiteSpace(relativeYear)) throw new ArgumentException("Invalid year format");

        double normalizedBirthYear = birthYear.NormalizedYearToBattleOfYavin();
        double normalizedRelativeYear = relativeYear.NormalizedYearToBattleOfYavin();
        return normalizedRelativeYear - normalizedBirthYear;
    }

    /// <summary>
    /// Calculates the average age of <paramref name="persons"/> relative to <paramref name="currentYear"/>
    /// </summary>
    /// <param name="persons"></param>
    /// <param name="currentYear"></param>
    /// <returns></returns>
    internal static double CalculateAverageAge(this IEnumerable<PersonDetailDTO> persons, string? currentYear)
    {
        if (string.IsNullOrWhiteSpace(currentYear)) return 0;

        int counter = 0;
        double sum = 0;
        foreach (PersonDetailDTO person in persons)
        {
            if (!string.IsNullOrWhiteSpace(person.BirthYear))
            {
                try
                {
                    double age = person.BirthYear.CalculateAge(currentYear);
                    if (age < 0) continue;
                    counter++;
                    sum += age;
                }
                catch
                {
                    continue;
                }
            }
        }

        return counter == 0 ? 0 : sum / counter;
    }
}
