using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Application.Careers.Dtos
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for the Content entity.
    /// It is used to transfer content data through the endpoint.
    /// </summary>
    public record AreaDto(string AreaDescription)
    {
        /// <summary>
        /// Creates a ContentDto object from a Content domain entity.
        /// </summary>
        /// <param name="content">The Content domain entity to convert to a ContentDto.</param>
        /// <returns>A ContentDto object containing the data from the provided Content entity.</returns>
        public static AreaDto FromArea(Area area)
        {
            return new AreaDto(area.Id.Value);
        }

        public static Area ToDomain(AreaDto areaDto)
        {
            return new Area(
                Domain.CareerAggregate.
                AreaDescription.Create(areaDto.AreaDescription)
            );
        }
    }
}
