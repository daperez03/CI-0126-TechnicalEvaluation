using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Domain.Services;

/// <summary>
/// Interface for a service responsible for calculating scholarship budgets.
/// </summary>
public interface IScholarshipCalculatorService
{
    /// <summary>
    /// Calculates the scholarship budget for a given career.
    /// </summary>
    /// <param name="career">The career for which the scholarship budget is calculated.</param>
    void Calculate(Career career);
}
