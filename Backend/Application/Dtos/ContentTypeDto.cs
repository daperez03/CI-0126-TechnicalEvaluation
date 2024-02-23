using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Application.Careers.Dtos;

/// <summary>
/// Represents a Data Transfer Object (DTO) for ContentType.
/// </summary>
public record ContentTypeDto(string Id)
{
    /// <summary>
    /// Creates a ContentTypeDto object from a ContentType domain entity.
    /// </summary>
    /// <param name="contentType">The ContentType domain entity to convert to a ContentTypeDto.</param>
    /// <returns>A ContentTypeDto object containing the data from the provided ContentType entity.</returns>
    public static ContentTypeDto FromContenType(ContentType contentType)
    {
        return new ContentTypeDto(contentType.Id.Value);
    }
}

