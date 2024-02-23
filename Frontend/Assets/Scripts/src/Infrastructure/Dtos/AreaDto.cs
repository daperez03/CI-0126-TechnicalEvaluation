using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Infrastructure
{
    public partial class AreaDto
    {
        public Area ToDomain()
        {
            var description =
                Domain.CareerAggregate.AreaDescription.Create(this.AreaDescription);
            return new Area(description);
        }

        public static AreaDto FromArea(Area area) 
        {
            var areaDto = new AreaDto();
            areaDto.AreaDescription = area.Id.Value;
            return areaDto;
        }
    }
}