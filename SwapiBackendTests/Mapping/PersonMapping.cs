using SwapiBackend.DTOs;
using SwapiBackend.Models;

namespace SwapiBackend.Mapping.Tests;

[TestClass]
public class PersonMapping
{
    private readonly PersonModel personModel = new() { Name = "Luke Skywalker", Height = "172", Mass = "77", HairColor = "blond", SkinColor = "fair", EyeColor = "blue", BirthYear = "19BBY" };

    private readonly AllPersonsModel allPersonsModel = new()
    {
        Count = 3,
        Next = null,
        Previous = null,
        Persons =
        [
            new PersonModel
            {
                Name = "Luke Skywalker",
                Height = "172",
                Mass = "77",
                HairColor = "blond",
                SkinColor = "fair",
                EyeColor = "blue",
                BirthYear = "19BBY",
            },
            new PersonModel
            {
                Name = "C-3PO",
                Height = "167",
                Mass = "75",
                HairColor = "n/a",
                SkinColor = "gold",
                EyeColor = "yellow",
                BirthYear = "112BBY",
            },
            new PersonModel
            {
                Name = "R2-D2",
                Height = "96",
                Mass = "32",
                HairColor = "n/a",
                SkinColor = "white, blue",
                EyeColor = "red",
                BirthYear = "33BBY",
            }
        ]
    };

    [TestMethod]
    public void ToPersonEntitiy()
    {
        // Arrange
        Assert.IsNotNull(personModel);
        PersonDTO result = personModel.ToPersonEntitiy();

        Assert.IsNotNull(result);
        Assert.AreEqual(personModel.Name, result.Name);
    }

    [TestMethod] public void ToPersonDetailDTO()
    {
        // Arrange
        Assert.IsNotNull(personModel);
        PersonDetailDTO result = personModel.ToPersonDetailEntity();

        Assert.IsNotNull(result);
        Assert.AreEqual(personModel.Name, result.Name);
        Assert.AreEqual(personModel.Height, result.Height);
        Assert.AreEqual(personModel.Mass, result.Mass);
        Assert.AreEqual(personModel.BirthYear, result.BirthYear);
        Assert.AreEqual(personModel.Gender, result.Gender);
    }


    [TestMethod]
    public void ToPersonEntities()
    {
        // Arrange
        Assert.IsNotNull(allPersonsModel.Persons);
        List<PersonDTO> result = allPersonsModel.Persons.ToPersonEntities();

        Assert.IsNotNull(result);
        Assert.AreEqual(allPersonsModel.Persons.Count, result.Count);

        for (int i = 0; i < allPersonsModel.Persons.Count; i++)
        {
            Assert.AreEqual(allPersonsModel.Persons[i].Name, result[i].Name);
        }
    }

    [TestMethod]
    public void ToPersonDetailEntities()
    {
        // Arrange
        Assert.IsNotNull(allPersonsModel.Persons);
        List<PersonDetailDTO> result = allPersonsModel.Persons.ToPersonDetailEntities();

        Assert.IsNotNull(result);
        Assert.AreEqual(allPersonsModel.Persons.Count, result.Count);

        for (int i = 0; i < allPersonsModel.Persons.Count; i++)
        {
            Assert.AreEqual(allPersonsModel.Persons[i].Name, result[i].Name);
            Assert.AreEqual(allPersonsModel.Persons[i].Height, result[i].Height);
            Assert.AreEqual(allPersonsModel.Persons[i].Mass, result[i].Mass);
            Assert.AreEqual(allPersonsModel.Persons[i].BirthYear, result[i].BirthYear);
            Assert.AreEqual(allPersonsModel.Persons[i].Gender, result[i].Gender);
        }
    }

    [TestMethod]
    public void UpdatePersonModel()
    {
        // Arrange
        PersonUpdateDTO personUpdateDTO = new
        (
            Name: "Luke Skywalker",
            Height: "250",
            BirthYear: "24BBY",
            Gender: "TestGender"
        );

        PersonModel personModelCopy = new() { Name = "Luke Skywalker", Height = "172", Mass = "77", HairColor = "blond", SkinColor = "fair", EyeColor = "blue", BirthYear = "19BBY", Gender="male" };
        Assert.AreEqual(personModelCopy.Name, "Luke Skywalker");
        Assert.AreEqual(personModelCopy.Height, "172");
        Assert.AreEqual(personModelCopy.BirthYear, "19BBY");
        Assert.AreEqual(personModelCopy.Gender, "male");

        personModelCopy.UpdatePersonModel(personUpdateDTO);
        Assert.AreEqual(personModelCopy.Name, "Luke Skywalker");
        Assert.AreEqual(personModelCopy.Height, "250");
        Assert.AreEqual(personModelCopy.BirthYear, "24BBY");
        Assert.AreEqual(personModelCopy.Gender, "TestGender");
    }

    [TestMethod]
    public void UpdatePersonModelWrong()
    {
        // Arrange
        PersonUpdateDTO personUpdateDTO = new
        (
            Name: "My Name",
            Height: "250",
            BirthYear: "24BBY",
            Gender: "TestGender"
        );

        PersonModel personModelCopy = new() { Name = "Luke Skywalker", Height = "172", Mass = "77", HairColor = "blond", SkinColor = "fair", EyeColor = "blue", BirthYear = "19BBY", Gender = "male" };
        Assert.AreEqual(personModelCopy.Name, "Luke Skywalker");
        Assert.AreEqual(personModelCopy.Height, "172");
        Assert.AreEqual(personModelCopy.BirthYear, "19BBY");
        Assert.AreEqual(personModelCopy.Gender, "male");

        Assert.ThrowsException<ArgumentException>(()=> personModelCopy.UpdatePersonModel(personUpdateDTO));
    }
}
