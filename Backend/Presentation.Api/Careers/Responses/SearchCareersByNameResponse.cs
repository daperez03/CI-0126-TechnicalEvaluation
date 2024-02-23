
using TechnicalEvaluation.Application.Careers.Dtos;

namespace TechnicalEvaluation.Presentation.Api.Careers.Responses
{
    /// <summary>
    /// Represents the response returned when searching for careers by name through the API.
    /// This response contains a list of careers that match the search criteria.
    /// </summary>
    public record SearchCareersByNameResponse(List<CareerDto> Careers);
}
