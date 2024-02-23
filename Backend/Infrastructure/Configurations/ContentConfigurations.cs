using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Infrastructure.Configurations;

/// <summary>
/// Entity mapping configuration for the Content class.
/// </summary>
public class ContentConfigurations : IEntityTypeConfiguration<Content>
{
    /// <summary>
    /// Configures the mapping for the Content entity.
    /// </summary>
    /// <param name="builder">Entity builder.</param>
    public void Configure(EntityTypeBuilder<Content> builder)
    {
        builder.ToTable("Content");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasConversion(
                convertToProviderExpression: id => id.Value,
                convertFromProviderExpression: id => ContentDescription.Create(id))
            .HasColumnName("Description");
        builder.Property(t => t.ContentType)
            .HasConversion(
                convertToProviderExpression: id => id.Value,
                convertFromProviderExpression: id => ContentTypeId.Create(id))
            .HasColumnName("ContentTypeId");
        builder.HasOne(t => t.Career)
            .WithMany(p => p.Contents)
            .HasForeignKey("CareerName");
        builder.HasOne<ContentType>()
               .WithMany()
               .HasForeignKey(c => c.ContentType)
               .OnDelete(DeleteBehavior.Cascade);

    }
}
