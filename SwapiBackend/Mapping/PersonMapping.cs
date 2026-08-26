using SwapiBackend.DTOs;
using SwapiBackend.Models;

namespace SwapiBackend.Mapping;

/// <summary>
/// Person mapping extension methods.
/// </summary>
internal static class PersonMapping
{
    /// <summary>
    /// Converts a <see cref="PersonModel"/> to a <see cref="PersonDTO"/>.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public static PersonDTO ToPersonEntitiy(this PersonModel model)
    {
        return new PersonDTO(model.Name);
    }

    /// <summary>
    /// Converts a <see cref="PersonModel"/> to a <see cref="PersonDetailDTO"/>."/>
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public static PersonDetailDTO ToPersonDetailEntity(this PersonModel model)
    {
        return new PersonDetailDTO(model.Name,
                                   model.Height,
                                   model.Mass,
                                   model.BirthYear,
                                   model.Gender);
    }

    /// <summary>
    /// Converts a list of <see cref="PersonModel"/> to a list of <see cref="PersonDTO"/>.
    /// </summary>
    /// <param name="models"></param>
    /// <returns></returns>
    public static List<PersonDTO> ToPersonEntities(this List<PersonModel> models)
    {
        return models.Select(model => model.ToPersonEntitiy()).ToList();
    }

    /// <summary>
    /// Converts a list of <see cref="PersonModel"/> to a list of <see cref="PersonDetailDTO"/>.
    /// </summary>
    /// <param name="models"></param>
    /// <returns></returns>
    public static List<PersonDetailDTO> ToPersonDetailEntities(this List<PersonModel> models)
    {
        return models.Select(model => model.ToPersonDetailEntity()).ToList();
    }

    /// <summary>
    /// Converts a <see cref="PersonDTO"/> to a <see cref="PersonModel"/>.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="updateDTO">Update person DTO to update <paramref name="model"/>.</param>
    /// <param name="fireExceptionIfNotSamePerson">If <see langword="true"/> the method is restrictive that both inputs has the same name.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Is fired if the <paramref name="model"/> and <paramref name="updateDTO"/> does not share the same name.</exception>
    public static PersonModel? UpdatePersonModel(this PersonModel model, PersonUpdateDTO updateDTO, bool fireExceptionIfNotSamePerson=true)
    {
        if (fireExceptionIfNotSamePerson && !model.IsSamePerson(updateDTO)) throw new ArgumentException("Person name does not match");

        model.BirthYear = updateDTO.BirthYear;
        model.Gender = updateDTO.Gender;
        model.Height = updateDTO.Height;
        return model;
    }
}
