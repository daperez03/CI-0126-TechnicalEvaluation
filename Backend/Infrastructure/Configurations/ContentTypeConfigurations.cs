
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Infrastructure.Configurations;

public class ContentTypeConfigurations : IEntityTypeConfiguration<ContentType>
{
    public void Configure(EntityTypeBuilder<ContentType> builder)
    {
        builder.ToTable("ContentType");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasConversion(
                convertToProviderExpression: id => id.Value,
                convertFromProviderExpression: id => ContentTypeId.Create(id))
            .HasColumnName("Id");
    }
}
