using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalEvaluation.Application.Repositories;
using TechnicalEvaluation.Domain.CareerAggregate;
using UnityEngine;
using Zenject;

namespace TechnicalEvaluation.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for managing Career entities in the database.
    /// </summary>
    public class CareerRepository : ICareerRepository
    {
        private readonly ApiClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="CareerRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public CareerRepository(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>
        /// Retrieves all Career entities from the database.
        /// </summary>
        /// <returns>A list of Career entities.</returns>
        public async Task<List<Career>> GetAllCareersAsync()
        {
            var careers = new List<Career>();
            try
            {
                var careersDtos = await _apiClient.CareersListGetAsync();
                foreach (var careerDto in careersDtos)
                {
                    careers.Add(careerDto.ToDomain());
                }
            }
            catch (ApiException ex)
            {
                Debug.Log(ex.Message);
            }
            return careers;
        }

        /// <summary>
        /// Retrieves a Career entity by its unique identifier.
        /// </summary>
        /// <param name="careerName">The unique identifier of the Career entity.</param>
        /// <returns>The Career entity.</returns>
        public async Task<Career?> GetByIdAsync(CareerName careerName)
        {
            Career? career = null;
            try
            {
                var response = await _apiClient.CareersInfoAsync(careerName.Value);
                career = response.Career?.ToDomain();
            }
            catch (ApiException ex)
            {
                Debug.Log(ex.Message);
            }
            return career;
        }

        /// <summary>
        /// Searches for Career entities by name.
        /// </summary>
        /// <param name="careerName">The name to search for.</param>
        /// <returns>A list of Career entities matching the provided name.</returns>
        public async Task<List<Career>> SearchCareersByName(CareerName careerName)
        {
            var careers = new List<Career>();
            try
            {
                var response = await _apiClient.CareersListGetAsync(careerName.Value);
                var careersDtos = response?.Careers;
                foreach (var careerDto in careersDtos ?? Enumerable.Empty<CareerDto>())
                {
                    careers.Add(careerDto.ToDomain());
                }
            }
            catch (ApiException ex)
            {
                Debug.Log(ex.Message);
            }
            return careers;
        }
    }
}