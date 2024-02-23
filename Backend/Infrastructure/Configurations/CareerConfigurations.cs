
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Infrastructure.Configurations;

/// <summary>
/// Entity mapping configuration for the Career class.
/// </summary>
public class CareerConfigurations : IEntityTypeConfiguration<Career>
{
    /// <summary>
    /// Configures the mapping for the Career entity.
    /// </summary>
    /// <param name="builder">Entity builder.</param>
    public void Configure(EntityTypeBuilder<Career> builder)
    {
        builder.ToTable("Career");
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .HasConversion(
                convertToProviderExpression: id => id.Value,
                convertFromProviderExpression: id => CareerName.Create(id))
            .HasColumnName("Name");
        builder.Property(p => p.WomenPercentage)
            .HasConversion(
                convertToProviderExpression: id => id.Value,
                convertFromProviderExpression: id => Percentage.Create(id))
            .HasColumnName("WomenPercentage");
        builder.Property(p => p.ScholarshipBudget)
            .HasConversion(
                convertToProviderExpression: id => id.Value,
                convertFromProviderExpression: id => Scholarship.Create(id))
            .HasColumnName("ScholarshipBudget");


        builder.HasMany(c => c.Areas)
            .WithMany(c => c.Careers)
            .UsingEntity<Dictionary<string, object>>(
                   "CareerAreas", // Table that joins the relationship
                   j => j.HasOne<Area>() // Set up of the Area part of the relationship
                        .WithMany()
                        .HasForeignKey("AreaType") // Specifying the foreign key
                        .OnDelete(DeleteBehavior.Cascade), 
                   j => j.HasOne<Career>() // Set up of the Area part of the relationship
                        .WithMany()
                        .HasForeignKey("CareerName") // Specifying the foreign key
                        .OnDelete(DeleteBehavior.Cascade)
        );

        builder
            .HasMany(c => c.Contents)
            .WithOne() // Assuming no navigation property back to Career
            .OnDelete(DeleteBehavior.Cascade);


    }
}
