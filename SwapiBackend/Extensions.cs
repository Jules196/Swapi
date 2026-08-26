using SwapiBackend.DTOs;
using SwapiBackend.Models;

namespace SwapiBackend;

/// <summary>
/// Async Extensions.
/// </summary>
internal static class AsyncExtensions
{
    /// <summary>
    /// Converts an <see cref="IAsyncEnumerable{T}"/> to a <see cref="List{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type of the list.</typeparam>
    /// <param name="items">IAsyncEnumerable which has to convert to <see cref="List{T}"/>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Returns <see cref="List{T}"/> with all elements if not cancelled by <paramref name="cancellationToken"/>.</returns>
    internal static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> items,
                                                       CancellationToken cancellationToken = default)
    {
        List<T> list = [];
        await foreach (var item in items.WithCancellation(cancellationToken)
                                          .ConfigureAwait(false))
        {
            list.Add(item);
        }
        return list;
    }
}

/// <summary>
/// Validation extensions.
/// </summary>
internal static class ValidationExtensions
{
    /// <summary>
    /// Validate <paramref name="personModel"/>.
    /// </summary>
    /// <param name="personModel"></param>
    /// <exception cref="ArgumentNullException">Throws if the <paramref name="personModel"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Throws if the <paramref name="personModel"/> is empty or only whitespace.</exception>
    internal static void ValidatePersonModel(this PersonModel? personModel)
    {
        if (personModel is null)
        {
            throw new ArgumentNullException(nameof(personModel));
        }
        else if (string.IsNullOrWhiteSpace(personModel.Name))
        {
            throw new ArgumentException("PersonModel.Name is null or empty.");
        }
    }

    /// <summary>
    /// Validate <paramref name="personUpdateDTO"/>.
    /// </summary>
    /// <param name="personUpdateDTO"></param>
    /// <exception cref="ArgumentNullException">Throws if the <paramref name="personUpdateDTO"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Throws if the <paramref name="personUpdateDTO"/> is empty or only whitespace.</exception>
    internal static void ValidatePersonUpdateDTO(this PersonUpdateDTO? personUpdateDTO)
    {
        if (personUpdateDTO is null)
        {
            throw new ArgumentNullException(nameof(personUpdateDTO));
        }
        else if (string.IsNullOrWhiteSpace(personUpdateDTO.Name))
        {
            throw new ArgumentException("PersonUpdateDTO.Name is null or empty.");
        }
    }
}

/// <summary>
/// Model and DTO comparision extensions.
/// </summary>
internal static class  ComparisionExtensions
{
    /// <summary>
    /// Compares if the <paramref name="model"/> and <paramref name="personDTO"/> are the same person.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="personDTO"></param>
    /// <returns></returns>
    internal static bool IsSamePerson(this PersonModel model, PersonDTO personDTO)
    {
        return string.Equals(model.Name, personDTO.Name);
    }

    /// <summary>
    /// Compares if the <paramref name="model"/> and <paramref name="personDetailDTO"/> are the same person.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="personDetailDTO"></param>
    /// <returns></returns>
    internal static bool IsSamePerson(this PersonModel model, PersonDetailDTO personDetailDTO)
    {
        return string.Equals(model.Name, personDetailDTO.Name);
    }

    /// <summary>
    /// Compares if the <paramref name="model"/> and <paramref name="personUpdateDTO"/> are the same person.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="personUpdateDTO"></param>
    /// <returns></returns>
    internal static bool IsSamePerson(this PersonModel model, PersonUpdateDTO personUpdateDTO)
    {
        return string.Equals(model.Name, personUpdateDTO.Name);
    }

    /// <summary>
    /// Compares if the <paramref name="personDTO"/> and <paramref name="model"/> are the same person.
    /// </summary>
    /// <param name="personDTO"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    internal static bool IsSamePerson(this PersonDTO personDTO, PersonModel model)
    {
        return string.Equals(personDTO.Name, model.Name);
    }

    /// <summary>
    /// Compares if the <paramref name="personDTO"/> and <paramref name="personDetailDTO"/> are the same person.
    /// </summary>
    /// <param name="personDTO"></param>
    /// <param name="personDetailDTO"></param>
    /// <returns></returns>
    internal static bool IsSamePerson(this PersonDTO personDTO, PersonDetailDTO personDetailDTO)
    {
        return string.Equals(personDTO.Name, personDetailDTO.Name);
    }

    /// <summary>
    /// Compares if the <paramref name="personDTO"/> and <paramref name="personUpdateDTO"/> are the same person.
    /// </summary>
    /// <param name="personDTO"></param>
    /// <param name="personUpdateDTO"></param>
    /// <returns></returns>
    internal static bool IsSamePerson(this PersonDTO personDTO, PersonUpdateDTO personUpdateDTO)
    {
        return string.Equals(personDTO.Name, personUpdateDTO.Name);
    }

    /// <summary>
    /// Compares if the <paramref name="personDetailDTO"/> and <paramref name="model"/> are the same person.
    /// </summary>
    /// <param name="personDetailDTO"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    internal static bool IsSamePerson(this PersonDetailDTO personDetailDTO, PersonModel model)
    {
        return string.Equals(personDetailDTO.Name, model.Name);
    }

    /// <summary>
    /// Compares if the <paramref name="personDetailDTO"/> and <paramref name="personDTO"/> are the same person.
    /// </summary>
    /// <param name="personDetailDTO"></param>
    /// <param name="personDTO"></param>
    /// <returns></returns>
    internal static bool IsSamePerson(this PersonDetailDTO personDetailDTO, PersonDTO personDTO)
    {
        return string.Equals(personDetailDTO.Name, personDTO.Name);
    }

    internal static bool IsSamePerson(this PersonDetailDTO personDetailDTO, PersonUpdateDTO personUpdateDTO)
    {
        return string.Equals(personDetailDTO.Name, personUpdateDTO.Name);
    }

    /// <summary>
    /// Compares if the <paramref name="personUpdateDTO"/> and <paramref name="model"/> are the same person.
    /// </summary>
    /// <param name="personUpdateDTO"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    internal static bool IsSamePerson(this PersonUpdateDTO personUpdateDTO, PersonModel model)
    {
        return string.Equals(personUpdateDTO.Name, model.Name);
    }

    /// <summary>
    /// Compares if the <paramref name="personUpdateDTO"/> and <paramref name="personDTO"/> are the same person.
    /// </summary>
    /// <param name="personUpdateDTO"></param>
    /// <param name="personDTO"></param>
    /// <returns></returns>
    internal static bool IsSamePerson(this PersonUpdateDTO personUpdateDTO, PersonDTO personDTO)
    {
        return string.Equals(personUpdateDTO.Name, personDTO.Name);
    }

    /// <summary>
    /// Compares if the <paramref name="personUpdateDTO"/> and <paramref name="personDetailDTO"/> are the same person.
    /// </summary>
    /// <param name="personUpdateDTO"></param>
    /// <param name="personDetailDTO"></param>
    /// <returns></returns>
    internal static bool IsSamePerson(this PersonUpdateDTO personUpdateDTO, PersonDetailDTO personDetailDTO)
    {
        return string.Equals(personUpdateDTO.Name, personDetailDTO.Name);
    }
}