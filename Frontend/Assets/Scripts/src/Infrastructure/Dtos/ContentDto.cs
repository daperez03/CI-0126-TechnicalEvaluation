using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Infrastructure
{
    public partial class ContentDto
    {
        public Content ToDomain()
        {
            var description = 
                Domain.CareerAggregate.ContentDescription.Create(this.ContentDescription);
            var contentType = ContentTypeId.Create(this.ContentType);
            return new Content(description, contentType);
        }

        public static ContentDto FromContent(Content content)
        {
            var contentDto = new ContentDto();
            contentDto.ContentDescription = content.Id.Value;
            contentDto.ContentType = content.ContentType.Value;
            return contentDto;
        }
    }
}