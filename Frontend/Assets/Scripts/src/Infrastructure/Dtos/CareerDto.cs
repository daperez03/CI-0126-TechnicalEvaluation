
using System.Linq;
using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Infrastructure
{
    public partial class CareerDto
    {
        public Career ToDomain()
        {
            var name = Domain.CareerAggregate.CareerName.Create(this.CareerName);
            var womenPercentage = Percentage.Create(this.WomenPercentage);
            var scholarshipBudget = Scholarship.Create(this.ScholarshipBudget);
            var career = new Career(name, womenPercentage, scholarshipBudget);
            foreach (var contentDto in this.Contents ?? Enumerable.Empty<ContentDto>())
            {
                career.AddContent(contentDto.ToDomain());
            }
            foreach (var areaDto in this.Areas ?? Enumerable.Empty<AreaDto>())
            {
                career.AddArea(areaDto.ToDomain());
            }
            return career;
        }

        public static CareerDto FromCareer(Career career)
        {
            var careerDto = new CareerDto();
            careerDto.CareerName = career.Id.Value;
            careerDto.WomenPercentage = career.WomenPercentage.Value;
            careerDto.ScholarshipBudget = career.ScholarshipBudget.Value;
            foreach (var content in career.Contents)
            {
                careerDto.Contents.Add(ContentDto.FromContent(content));
            }
            foreach (var area in career.Areas)
            {
                careerDto.Areas.Add(AreaDto.FromArea(area));
            }
            return careerDto;
        }
    }
}