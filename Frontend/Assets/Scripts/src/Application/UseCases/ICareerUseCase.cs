using System.Collections.Generic;
using System.Threading.Tasks;
using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Application.UseCases
{
    /// <summary>
    /// Represents an interface for use cases related to Career entities.
    /// </summary>
    public interface ICareerUseCase
    {
        /// <summary>
        /// Searches for Career entities by name.
        /// </summary>
        /// <param name="careerName">The name to search for.</param>
        /// <returns>A list of Career entities matching the provided name.</returns>
        public Task<List<Career>> SearchCareersByNameAsync(string careerName);

        /// <summary>
        /// Retrieves a Career entity by its unique identifier.
        /// </summary>
        /// <param name="careerName">The unique identifier of the Career entity.</param>
        /// <returns>The Career entity</returns>
        public Task<Career?> GetCareerByIdAsync(string careerName);

        /// <summary>
        /// Asynchronously retrieves all Career entities.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, returning a list of Career entities.</returns>
        public Task<List<Career>> GetAllCareersAsync();


        public void ShowCareers(List<Career> careers);
    }
}

