using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalEvaluation.Application.Presenters;
using TechnicalEvaluation.Application.Repositories;
using TechnicalEvaluation.Domain.CareerAggregate;
using UnityEngine;
using Zenject;

namespace TechnicalEvaluation.Application.UseCases
{
    /// <summary>
    /// Represents a use case for managing Career entities.
    /// </summary>
    public class CareerUseCase : ICareerUseCase
    {
        [Inject]
        private readonly ICareerRepository _careerRepository;

        [InjectOptional]
        private readonly ICareerUIPresenter _careerUIPresenter;

        /// <summary>
        /// Initializes a new instance of the <see cref="CareerUseCase"/> class.
        /// </summary>
        /// <param name="careerRepository">The career repository to interact with Career entities.</param>
        public CareerUseCase(ICareerRepository careerRepository)
        {
            _careerRepository = careerRepository;
        }


        /// <summary>
        /// Searches for Career entities by name.
        /// </summary>
        /// <param name="careerName">The name to search for.</param>
        /// <returns>A list of Career entities matching the provided name.</returns>
        public async Task<List<Career>> SearchCareersByNameAsync(string careerName)
        {
            var careerId = CareerName.Create(careerName);

            return await _careerRepository.SearchCareersByName(careerId);
        }

        /// <summary>
        /// Retrieves a Career entity by its unique identifier.
        /// </summary>
        /// <param name="careerName">The unique identifier of the Career entity.</param>
        /// <returns>The Career entity.</returns>
        public async Task<Career?> GetCareerByIdAsync(string careerName)
        {
            var careerId = CareerName.Create(careerName);
            return await _careerRepository.GetByIdAsync(careerId);
        }

        /// <summary>
        /// Asynchronously retrieves all Career entities.
        /// </summary>
        /// <returns> A task representing the asynchronous operation, returning a list of Career entities.</returns>
        public async Task<List<Career>> GetAllCareersAsync()
        {
            return await _careerRepository.GetAllCareersAsync();
        }

        public void ShowCareers(List<Career> careers)
        {
            if (_careerUIPresenter is not null)
            {
                _careerUIPresenter.ClearContents();
                foreach(var career in careers)
                {
                    _careerUIPresenter.Render(career);
                }
            } else
            {
                Debug.LogError("The CareerUIPresenter was not found!");
            }

        }

        protected CareerUseCase()
        {

        }
    }

}

