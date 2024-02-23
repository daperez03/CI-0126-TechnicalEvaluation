using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TechnicalEvaluation.Application.Repositories;
using TechnicalEvaluation.Infrastructure.Repositories;

namespace TechnicalEvaluation.Infrastructure
{
    /// <summary>
    /// Provides extension method to register infrastructure layer services.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers infrastructure layer services to the provided IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the services to.</param>
        /// <returns>The same service collection with infrastructure layer services added.</returns>
        public static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection services)
        {
            // Registers the ApplicationDbContext to the service collection and configures it to use SQL Server
            // with the provided connection string.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Initial Catalog = TechnicalEvaluation.Database; Integrated Security = true;")
                );

            services.AddScoped<ICareerRepository, CareerRepository>();

            return services;
        }
    }
}
