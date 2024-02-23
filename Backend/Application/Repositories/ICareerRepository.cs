using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Application.Repositories;

/// <summary>
/// Represents a repository interface for managing Career entities.
/// </summary>
public interface ICareerRepository : IRepository<Career, CareerName>
{
    /// <summary>
    /// Retrieves a Career entity by its unique identifier.
    /// </summary>
    /// <param name="careerName">The identifier of the Career entity.</param>
    /// <returns>The Career entity.</returns>
    public Task<Career?> GetByIdAsync(CareerName careerName);

    /// <summary>
    /// Retrieves all Career entities.
    /// </summary>
    /// <returns>A list of Career entities.</returns>
    public Task<List<Career>> GetAllCareersAsync();

    /// <summary>
    /// Creates a new Career entity in the repository.
    /// </summary>
    /// <param name="career">The Career entity to create.</param>
    public Task CreateCareerAsync(Career career);

    /// <summary>
    /// Updates an existing Career entity in the repository.
    /// </summary>
    /// <param name="career">The Career entity to update.</param>
    public Task UpdateCareerAsync(Career career, bool isUpdate = false);

    /// <summary>
    /// Searches for Career entities by name.
    /// </summary>
    /// <param name="careerName">The name to search for.</param>
    /// <returns>A list of Career entities matching the provided name.</returns>
    public Task<List<Career>> SearchCareersByName(CareerName careerName);

    /// <summary>
    /// Asynchronously retrieves all ContentType entities.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, returning a list of ContentType entities.</returns>
    public Task<List<ContentType>> GetAllContentTypesAsync();

    public void Update<Type>(Type obj);




}
