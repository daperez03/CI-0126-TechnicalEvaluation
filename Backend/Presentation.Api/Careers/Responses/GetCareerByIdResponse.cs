
using TechnicalEvaluation.Application.Careers.Dtos;

namespace TechnicalEvaluation.Presentation.Api.Careers.Responses
{
    /// <summary>
    /// Represents the response returned when fetching a career by its ID through the API.
    /// This response includes the details of the requested career along with all of its contents.
    /// </summary>
    public record GetCareerByIdResponse(CareerDto Career); // Inside the career will be all of the contents.
}
