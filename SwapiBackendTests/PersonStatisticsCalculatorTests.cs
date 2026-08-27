using SwapiBackend.DTOs;

namespace SwapiBackend.Tests;

[TestClass]
public class PersonStatisticsCalculatorTests
{
    [TestMethod]
    public void TryCalculateReturnsAveragesAndPercentages()
    {
        PersonDetailDTO[] people =
        [
            new("Luke Skywalker", "172", BirthYear: "19BBY", Gender: "male"),
            new("Leia Organa", "150", BirthYear: "19BBY", Gender: "female"),
            new("R2-D2", "unknown", BirthYear: "unknown", Gender: "n/a"),
        ];

        bool success = PersonStatisticsCalculator.TryCalculate(
            people,
            "24ABY",
            out PersonStatistics statistics);

        Assert.IsTrue(success);
        Assert.AreEqual(161, statistics.AverageHeight);
        Assert.AreEqual(43, statistics.AverageAge);
        Assert.AreEqual(50, statistics.MalePercentage);
        Assert.AreEqual(50, statistics.FemalePercentage);
    }

    [TestMethod]
    public void TryCalculateRejectsInvalidCurrentYear()
    {
        bool success = PersonStatisticsCalculator.TryCalculate(
            [],
            "24",
            out PersonStatistics statistics);

        Assert.IsFalse(success);
        Assert.AreEqual(new PersonStatistics(0, 0, 0, 0), statistics);
    }
}
