using System.Globalization;
using SwapiBackend.DTOs;

namespace SwapiBackend;

public sealed record PersonStatistics(
    double AverageHeight,
    double AverageAge,
    double MalePercentage,
    double FemalePercentage);

public static class PersonStatisticsCalculator
{
    public static bool TryCalculate(
        IEnumerable<PersonDetailDTO> people,
        string currentYear,
        out PersonStatistics statistics)
    {
        statistics = new PersonStatistics(0, 0, 0, 0);

        if (!TryNormalizeYear(currentYear, out double normalizedCurrentYear))
        {
            return false;
        }

        List<double> heights = [];
        List<double> ages = [];
        int maleCount = 0;
        int femaleCount = 0;

        foreach (PersonDetailDTO person in people)
        {
            if (TryParseNumber(person.Height, out double height))
            {
                heights.Add(height);
            }

            if (TryNormalizeYear(person.BirthYear, out double normalizedBirthYear))
            {
                double age = normalizedCurrentYear - normalizedBirthYear;
                if (age >= 0)
                {
                    ages.Add(age);
                }
            }

            if (string.Equals(person.Gender, "male", StringComparison.OrdinalIgnoreCase))
            {
                maleCount++;
            }
            else if (string.Equals(person.Gender, "female", StringComparison.OrdinalIgnoreCase))
            {
                femaleCount++;
            }
        }

        int binaryGenderCount = maleCount + femaleCount;
        statistics = new PersonStatistics(
            Average(heights),
            Average(ages),
            binaryGenderCount == 0 ? 0 : maleCount * 100d / binaryGenderCount,
            binaryGenderCount == 0 ? 0 : femaleCount * 100d / binaryGenderCount);
        return true;
    }

    private static double Average(List<double> values) =>
        values.Count == 0 ? 0 : values.Average();

    private static bool TryNormalizeYear(string? year, out double normalizedYear)
    {
        normalizedYear = 0;
        if (string.IsNullOrWhiteSpace(year))
        {
            return false;
        }

        ReadOnlySpan<char> normalized = year.AsSpan().Trim();
        if (normalized.Length <= 3)
        {
            return false;
        }

        ReadOnlySpan<char> era = normalized[^3..];
        if (!TryParseNumber(normalized[..^3], out double value))
        {
            return false;
        }

        if (era.Equals("BBY", StringComparison.OrdinalIgnoreCase))
        {
            normalizedYear = -value;
            return true;
        }

        if (era.Equals("ABY", StringComparison.OrdinalIgnoreCase))
        {
            normalizedYear = value;
            return true;
        }

        return false;
    }

    private static bool TryParseNumber(string? value, out double number) =>
        double.TryParse(
            value,
            NumberStyles.Float,
            CultureInfo.InvariantCulture,
            out number);

    private static bool TryParseNumber(ReadOnlySpan<char> value, out double number) =>
        double.TryParse(
            value,
            NumberStyles.Float,
            CultureInfo.InvariantCulture,
            out number);
}
