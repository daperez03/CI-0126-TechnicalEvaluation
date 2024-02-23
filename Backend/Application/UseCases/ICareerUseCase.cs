using TechnicalEvaluation.Application.Careers.Dtos;
using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Application.UseCases;

/// <summary>
/// Represents an interface for use cases related to Career entities.
/// </summary>
public interface ICareerUseCase
{
    /// <summary>
    /// Creates a new Career entity.
    /// </summary>
    /// <param name="careerName">The name of the Career to create.</param>
    /// <returns>The newly created Career entity.</returns>
    public Task<Career> CreateCareerAsync(CareerDto careerDto);

    /// <summary>
    /// Asynchronously adds content to a career.
    /// </summary>
    /// <param name="careerName">The name of the career to which the content will be added.</param>
    /// <param name="contentDescription">The description of the content to be added.</param>
    /// <param name="contentType">The type of the content to be added.</param>
    /// <returns>A task representing the asynchronous operation, returning the updated Career object.</returns>
    public Task<Career> AddContentToCareerAsync(string careerName, string contentDescription, string contentType);

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
    /// Asynchronously retrieves all ContentType entities.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, returning a list of ContentType entities.</returns>
    public Task<List<ContentType>> GetAllContentTypesAsync();

    /// <summary>
    /// Asynchronously updates a Career entity.
    /// </summary>
    /// <param name="career">The Career entity to be updated.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task UpdateCareerAsync(CareerDto careerDto);

    /// <summary>
    /// Asynchronously retrieves all Career entities.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, returning a list of Career entities.</returns>
    public Task<List<Career>> GetAllCareersAsync();
}
