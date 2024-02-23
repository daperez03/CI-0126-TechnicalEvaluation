using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Application.Careers.Dtos
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for the Content entity.
    /// It is used to transfer content data through the endpoint.
    /// </summary>
    public record ContentDto(string ContentDescription, string ContentType)
    {
        /// <summary>
        /// Converts a Content domain entity to a ContentDto object.
        /// </summary>
        /// <param name="content">The Content domain entity to be converted.</param>
        /// <returns>A ContentDto object representing the data from the provided Content entity.</returns>
        public static ContentDto FromContent(Content content)
        {
            return new ContentDto(content.Id.Value, content.ContentType.Value);
        }

        public static Content ToDomain(ContentDto contentDto)
        {
            return new Content(
                Domain.CareerAggregate.
                ContentDescription.Create(contentDto.ContentDescription),
                ContentTypeId.Create(contentDto.ContentType)
            );
        }
    }
}
