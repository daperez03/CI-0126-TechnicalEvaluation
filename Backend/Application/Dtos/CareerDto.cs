using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Application.Careers.Dtos;

/// <summary>
/// Represents a Data Transfer Object (DTO) for the Career entity.
/// It is used to transfer career data through the endpoint.
/// </summary>
public record CareerDto(
    string CareerName,
    float WomenPercentage,
    double ScholarshipBudget,
    List<ContentDto> Contents, 
    List<AreaDto> Areas)
{
    /// <summary>
    /// Creates a CareerDto object from a Career domain entity.
    /// </summary>
    /// <param name="career">The Career domain entity to convert to a CareerDto.</param>
    /// <returns>A CareerDto object containing the data from the provided Career entity.</returns>
    public static CareerDto FromCareer(Career career)
    {
        return new CareerDto(
            career.Id.Value,
            career.WomenPercentage.Value,
            career.ScholarshipBudget.Value,
            career.Contents
                .Select(c => ContentDto.FromContent(c))
                .ToList(),
            career.Areas
                .Select(a => AreaDto.FromArea(a))
                .ToList());
    }

    public static Career ToDomain(CareerDto careerDto)
    {
        var career = new Career(
            Domain.CareerAggregate.CareerName.Create(careerDto.CareerName),
            Percentage.Create(careerDto.WomenPercentage),
            Scholarship.Create(careerDto.ScholarshipBudget)
        );
        
        foreach (var area in careerDto.Areas)
        {
            career.AddArea(AreaDto.ToDomain(area));
        }

        foreach (var content in careerDto.Contents)
        {
            career.AddContent(ContentDto.ToDomain(content));
        }
        return career;
    }
}
