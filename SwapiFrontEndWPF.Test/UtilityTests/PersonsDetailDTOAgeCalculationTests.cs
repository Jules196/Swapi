using SwapiFrontEndWPF.Utilities;
using SwapiBackend.DTOs;

namespace SwapiFrontEndWPF.Test;

[TestClass]
public class PersonsDetailDTOAgeCalculationTests
{
    private const string _failYear = "";
    private const string _failBbyYear = "BBY";
    private const string _failAbyYear = "ABY";
    private const string _failYearWithLetters = "ABY24";

    private const string _beforeBattleOfYavin = "24BBY";
    private const string _afterBattleOfYavin = "24ABY";

    [TestMethod]
    public void NormalizedYearToBattleOfYavinTest()
    {
        Assert.ThrowsException<ArgumentException>(() => _failYear.NormalizedYearToBattleOfYavin());
        Assert.ThrowsException<ArgumentException>(() => _failBbyYear.NormalizedYearToBattleOfYavin());
        Assert.ThrowsException<ArgumentException>(() => _failAbyYear.NormalizedYearToBattleOfYavin());
        Assert.ThrowsException<ArgumentException>(() => _failYearWithLetters.NormalizedYearToBattleOfYavin());

        Assert.AreEqual(-24, _beforeBattleOfYavin.NormalizedYearToBattleOfYavin());
        Assert.AreEqual(24, _afterBattleOfYavin.NormalizedYearToBattleOfYavin());
    }

    [TestMethod]
    public void CalculateAgeTest()
    {
        Assert.ThrowsException<ArgumentException>(() => _failYear.CalculateAge(_afterBattleOfYavin));
        Assert.ThrowsException<ArgumentException>(() => _beforeBattleOfYavin.CalculateAge(_failYear));
        Assert.ThrowsException<ArgumentException>(() => _failYear.CalculateAge(_failYear));

        Assert.AreEqual(48, _beforeBattleOfYavin.CalculateAge(_afterBattleOfYavin));
        Assert.AreEqual(0, _afterBattleOfYavin.CalculateAge(_afterBattleOfYavin));
    }

    [TestMethod]
    public void CalculateAverageAgeTest()
    {
        List<PersonDetailDTO> persons =
        [
            new PersonDetailDTO (Name : "Luke", BirthYear : "24BBY"),
            new PersonDetailDTO (Name : "Luke", BirthYear : "24BBY"),
            new PersonDetailDTO (Name : "Luke", BirthYear : "24BBY"),
            new PersonDetailDTO (Name : "Luke", BirthYear : "24BBY")
        ];

        Assert.AreEqual(48, persons.CalculateAverageAge(_afterBattleOfYavin));
    }
}