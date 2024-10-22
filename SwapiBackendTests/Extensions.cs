using SwapiBackend.DTOs;
using SwapiBackend.Models;

namespace SwapiBackend.Tests;

[TestClass]
public class Extensions
{
    [TestMethod]
    public void ToListAsyncTest()
    {
        async static IAsyncEnumerable<int> GetElements(int count)
        {
            for (int i = 0; i < count; i++)
            {
                await Task.Delay(50);
                yield return i;
            }
        }
        int count = 10;
        IAsyncEnumerable<int> elements = GetElements(count);
        List<int> list = elements.ToListAsync().GetAwaiter().GetResult();

        Assert.AreEqual(count, list.Count);
    }

    [TestMethod]
    public void IsPersonModelValidTest()
    {
        PersonModel? personModel = null;
        Assert.ThrowsException<ArgumentNullException>(() => personModel.ValidatePersonModel());

        personModel = new PersonModel() { Name = "" };
        Assert.ThrowsException<ArgumentException>(() => personModel.ValidatePersonModel());

        personModel = new PersonModel() { Name = " " };
        Assert.ThrowsException<ArgumentException>(() => personModel.ValidatePersonModel());

        personModel = new PersonModel() { Name = "Luke Skywalker" };
        try
        {
            personModel.ValidatePersonModel();
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [TestMethod]
    public void IsPersonUpdateDTOValidTest()
    {
        PersonUpdateDTO? personUpdateDTO = null;
        Assert.ThrowsException<ArgumentNullException>(() => personUpdateDTO.ValidatePersonUpdateDTO());

        personUpdateDTO = new PersonUpdateDTO("", "", "","");
        Assert.ThrowsException<ArgumentException>(() => personUpdateDTO.ValidatePersonUpdateDTO());

        personUpdateDTO = new PersonUpdateDTO(" ", " ", " ", " ");
        Assert.ThrowsException<ArgumentException>(() => personUpdateDTO.ValidatePersonUpdateDTO());

        personUpdateDTO = new PersonUpdateDTO("Luke Skywalker", "", "", "");
        try
        {
            personUpdateDTO.ValidatePersonUpdateDTO();
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [TestMethod]
    public void IsSamePersonTests()
    {
        PersonModel samePersonModel = new PersonModel() { Name = "Luke Skywalker" };
        PersonDTO samePersonDTO = new PersonDTO("Luke Skywalker");
        PersonDetailDTO samePersonDetailDTO = new PersonDetailDTO("Luke Skywalker", "172", "77", "19BB", "");
        PersonUpdateDTO samePersonUpdateDTO = new PersonUpdateDTO("Luke Skywalker", "172", "19BBY", "");

        PersonModel diffPersonModel = new PersonModel() { Name = "Other Name" };
        PersonDTO diffPersonDTO = new PersonDTO("Other Name");
        PersonDetailDTO diffPersonDetailDTO = new PersonDetailDTO("Other Name", "172", "77", "19BB", "");
        PersonUpdateDTO diffPersonUpdateDTO = new PersonUpdateDTO("Other Name", "172", "19BBY", "");

        // Is True tests
        Assert.IsTrue(samePersonModel.IsSamePerson(samePersonDTO));
        Assert.IsTrue(samePersonModel.IsSamePerson(samePersonDetailDTO));
        Assert.IsTrue(samePersonModel.IsSamePerson(samePersonUpdateDTO));

        Assert.IsTrue(samePersonDTO.IsSamePerson(samePersonModel));
        Assert.IsTrue(samePersonDTO.IsSamePerson(samePersonDetailDTO));
        Assert.IsTrue(samePersonDTO.IsSamePerson(samePersonUpdateDTO));

        Assert.IsTrue(samePersonDetailDTO.IsSamePerson(samePersonModel));
        Assert.IsTrue(samePersonDetailDTO.IsSamePerson(samePersonDTO));
        Assert.IsTrue(samePersonDetailDTO.IsSamePerson(samePersonUpdateDTO));

        Assert.IsTrue(samePersonUpdateDTO.IsSamePerson(samePersonModel));
        Assert.IsTrue(samePersonUpdateDTO.IsSamePerson(samePersonDTO));
        Assert.IsTrue(samePersonUpdateDTO.IsSamePerson(samePersonDetailDTO));

        // Is False tests
        Assert.IsFalse(samePersonModel.IsSamePerson(diffPersonDTO));
        Assert.IsFalse(samePersonModel.IsSamePerson(diffPersonDetailDTO));
        Assert.IsFalse(samePersonModel.IsSamePerson(diffPersonUpdateDTO));

        Assert.IsFalse(samePersonDTO.IsSamePerson(diffPersonModel));
        Assert.IsFalse(samePersonDTO.IsSamePerson(diffPersonDetailDTO));
        Assert.IsFalse(samePersonDTO.IsSamePerson(diffPersonUpdateDTO));

        Assert.IsFalse(samePersonDetailDTO.IsSamePerson(diffPersonModel));
        Assert.IsFalse(samePersonDetailDTO.IsSamePerson(diffPersonDTO));
        Assert.IsFalse(samePersonDetailDTO.IsSamePerson(diffPersonUpdateDTO));

        Assert.IsFalse(samePersonUpdateDTO.IsSamePerson(diffPersonModel));
        Assert.IsFalse(samePersonUpdateDTO.IsSamePerson(diffPersonDTO));
        Assert.IsFalse(samePersonUpdateDTO.IsSamePerson(diffPersonDetailDTO));
    }
}
