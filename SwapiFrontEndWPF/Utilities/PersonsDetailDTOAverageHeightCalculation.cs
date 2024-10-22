using SwapiBackend.DTOs;

namespace SwapiFrontEndWPF.Utilities;

/// <summary>
/// Height calcultation extensions.
/// </summary>
internal static class PersonsDetailDTOAverageHeightCalculation
{
    /// <summary>
    /// Calculates the average height of <paramref name="persons"/>
    /// </summary>
    /// <param name="persons"></param>
    /// <returns></returns>
    internal static double CalculateAverageHeight(this IEnumerable<PersonDetailDTO> persons)
    {
        int counter = 0;
        double sum = 0;
        foreach (PersonDetailDTO person in persons)
        {
            if (!string.IsNullOrWhiteSpace(person.Height)
                && double.TryParse(person.Height, out double val))
            {
                counter++;
                sum += val;
            }
        }

        return counter == 0 ? 0 : sum / counter;
    }
}
