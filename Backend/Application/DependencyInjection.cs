using Microsoft.Extensions.DependencyInjection;
using TechnicalEvaluation.Application.UseCases;
using TechnicalEvaluation.Domain.Services;

namespace TechnicalEvaluation.Application
{
    /// <summary>
    /// Provides an extension method to register application layer services.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers application layer services to the provided IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to which the services are added.</param>
        /// <returns>The same service collection with application layer services added.</returns>
        public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
        {
            services.AddScoped<ICareerUseCase, CareerUseCase>();
            services.AddScoped<IScholarshipCalculatorService, ScholarshipCalculatorService>();
            return services;
        }
    }
}
