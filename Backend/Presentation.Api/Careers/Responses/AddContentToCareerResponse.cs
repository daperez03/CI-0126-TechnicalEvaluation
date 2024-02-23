
using TechnicalEvaluation.Application.Careers.Dtos;

namespace TechnicalEvaluation.Presentation.Api.Careers.Responses
{
    /// <summary>
    /// Represents the response returned after adding content to a career.
    /// This response includes details of the career to which content has been added.
    /// </summary>
    public record AddContentToCareerResponse(CareerDto Career);
}
