using SwapiBackend.DTOs;
using SwapiFrontEndWPF.Utilities;

namespace SwapiFrontEndWPF.UtilityTests;

[TestClass]
public class PersonsDetailDTOAverageHeightCalculationTest
{
    [TestMethod]
    public void CalculateAverageHeightTest()
    {
        // Arrange
        var persons = new List<PersonDetailDTO>
        {
            new PersonDetailDTO("Luke Skywalker", "172"),
            new PersonDetailDTO("Luke Skywalker", "172"),
            new PersonDetailDTO("Luke Skywalker", "172"),

        };

        Assert.AreEqual(172, persons.CalculateAverageHeight());
    }
}
