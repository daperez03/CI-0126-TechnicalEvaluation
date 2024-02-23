using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Infrastructure;

/// <summary>
/// Represents the database context for the application.
/// </summary>
public class ApplicationDbContext : DbContext
{
    [Obsolete("Use for Moq")]
    public ApplicationDbContext()
    : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    /// <param name="options">The database context options.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>(options);
            optionsBuilder.EnableSensitiveDataLogging();

            // Update the internal options field with the new options
            var optionsField = typeof(DbContext).GetField("_options", BindingFlags.NonPublic | BindingFlags.Instance);
            optionsField?.SetValue(this, optionsBuilder.Options);

    }

    /// <summary>
    /// Gets or sets the DbSet for the Career entity.
    /// </summary>
    public virtual DbSet<Career> Careers { get; set; }
    /// <summary>
    /// Gets or sets the DbSet for the Content entity.
    /// </summary>
    public virtual DbSet<Content> Contents { get; set; }
    /// <summary>
    /// Gets or sets the DbSet for the ContentType entity in the application's DbContext.
    /// </summary>
    public virtual DbSet<ContentType> ContentTypes { get; set; }



    /// <summary>
    /// Gets or sets the DbSet for the Area entity.
    /// </summary>
    public virtual DbSet<Area> Areas { get; set; }


    /// <summary>
    /// Configures the database model during initialization.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
    }
}
