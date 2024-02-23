using TechnicalEvaluation.Domain.CareerAggregate;
using TechnicalEvaluation.Domain.Services;

namespace TechnicalEvaluation.Domain.Services;

/// <summary>
/// Service for calculating scholarship budgets based on career criteria.
/// </summary>
public class ScholarshipCalculatorService : IScholarshipCalculatorService
{
    /// <summary>
    /// Calculates the scholarship budget for a career.
    /// </summary>
    /// <param name="career">The career for which the scholarship budget is calculated.</param>
    public void Calculate(Career career)
    {
        CalculateBase(career);
        CalculateAccumulate(career);
    }

    /// <summary>
    /// Calculates the base scholarship budget for a career.
    /// </summary>
    /// <param name="career">The career for which the base scholarship budget is calculated.</param>
    private void CalculateBase(Career career)
    {
        var baseBudget = 0.0;

        foreach (var content in career.Contents)
        {
            if (content.ContentType.Value == "Tecnologico" ||
                content.ContentType.Value == "Ambiental" ||
                content.ContentType.Value == "Social")
            {
                baseBudget += 100;

                if (content.ContentType.Value == "Tecnologico")
                {
                    baseBudget += 100;
                }
            }
        }

        career.ScholarshipBudget = Scholarship.Create(baseBudget);
    }

    /// <summary>
    /// Calculates additional scholarship amounts based on career criteria.
    /// </summary>
    /// <param name="career">The career for which additional scholarship amounts are calculated.</param>
    private void CalculateAccumulate(Career career)
    {
        var baseBudget = career.ScholarshipBudget.Value;
        var isStem = false;
        var isComputer = false;

        foreach (var area in career.Areas)
        {
            if (area.Id.Value == "Ciencia" ||
                area.Id.Value == "Tecnologia" ||
                area.Id.Value == "Ingenieria" ||
                area.Id.Value == "Matematica")
            {
                isStem = true;
            }

            if (area.Id.Value == "Computacion e Informatica")
            {
                isComputer = true;
            }
        }

        double accumulate = baseBudget * 0.2;

        if (isStem)
        {
            accumulate += baseBudget * 0.3;
            accumulate += accumulate * 0.1;
        }

        if (career.WomenPercentage.Value > 50)
        {
            accumulate += baseBudget * 0.1;

            if (isStem)
            {
                accumulate += accumulate * 0.08;
            }
        }

        if (isComputer)
        {
            accumulate += accumulate * 0.05;
        }

        career.ScholarshipBudget = Scholarship.Create(baseBudget + accumulate);
    }
}

