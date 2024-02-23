
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Numerics;
using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Infrastructure.Configurations;

/// <summary>
/// Entity mapping configuration for the Career class.
/// </summary>
public class AreaConfigurations : IEntityTypeConfiguration<Area>
{
    /// <summary>
    /// Configures the mapping for the Career entity.
    /// </summary>
    /// <param name="builder">Entity builder.</param>
    public void Configure(EntityTypeBuilder<Area> builder)
    {
        builder.ToTable("Area");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
            .HasConversion(
                convertToProviderExpression: id => id.Value,
                convertFromProviderExpression: id => AreaDescription.Create(id)
            )
            .HasColumnName("Type");

    }
}
