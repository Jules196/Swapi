using SwapiBackend.DTOs;
using SwapiFrontEndWPF.Utilities;

namespace SwapiFrontEndWPF.UtilityTests.Test;

[TestClass]
public class PersonsDetailDTOGenderCalculationTest
{
    [TestMethod]
    public void CalculateGenderTest()
    {
        // Arrange
        var persons = new List<PersonDetailDTO>
        {
            new PersonDetailDTO("Luke Skywalker", Gender: "male"),
            new PersonDetailDTO("Luke Skywalker", Gender: "male"),
            new PersonDetailDTO("Luke Skywalker", Gender: "male"),
            new PersonDetailDTO("Lucy Skywalker", Gender: "female"),
            new PersonDetailDTO("Lucy Skywalker", Gender: "female"),
            new PersonDetailDTO("Lucy Skywalker", Gender: "female"),
            new PersonDetailDTO("R2D2", Gender: ""),
            new PersonDetailDTO("R2D2", Gender: "")
        };

        (double male, double female) = persons.CountMalesAndFemales();
        Assert.AreEqual(3, male);
        Assert.AreEqual(3, female);
    }
}
