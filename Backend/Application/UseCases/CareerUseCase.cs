using TechnicalEvaluation.Application.Careers.Dtos;
using TechnicalEvaluation.Application.Repositories;
using TechnicalEvaluation.Domain.CareerAggregate;
using TechnicalEvaluation.Domain.Services;

namespace TechnicalEvaluation.Application.UseCases;

/// <summary>
/// Represents a use case for managing Career entities.
/// </summary>
public class CareerUseCase : ICareerUseCase
{
    private readonly ICareerRepository _careerRepository;
    private readonly IScholarshipCalculatorService _scholarshipCalculaterService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CareerUseCase"/> class.
    /// </summary>
    /// <param name="careerRepository">The career repository to interact with Career entities.</param>
    public CareerUseCase(
        ICareerRepository careerRepository,
        IScholarshipCalculatorService scholarshipCalculaterService)
    {
        _careerRepository = careerRepository;
        _scholarshipCalculaterService = scholarshipCalculaterService;
    }

    /// <summary>
    /// Asynchronously adds content to a career.
    /// </summary>
    /// <param name="careerName">The name of the career to which the content will be added.</param>
    /// <param name="contentDescription">The description of the content to be added.</param>
    /// <param name="contentType">The type of the content to be added.</param>
    /// <returns>A task representing the asynchronous operation, returning the updated Career object.</returns>
    public async Task<Career> AddContentToCareerAsync(
        string careerName,
        string contentDescription,
        string contentType)
    {
        var careerId = CareerName.Create(careerName);

        var career = await _careerRepository.GetByIdAsync(careerId)
            ?? throw new Exception("Career not found");

        var id = ContentDescription.Create(contentDescription);
        var type = ContentTypeId.Create(contentType);

        var content = new Content(id, type);

        career.AddContent(content);

        _scholarshipCalculaterService.Calculate(career);

        await _careerRepository.UpdateCareerAsync(career);

        return career;
    }

    /// <summary>
    /// Creates a new Career entity.
    /// </summary>
    /// <param name="careerName">The name of the Career to create.</param>
    /// <returns>The newly created Career entity.</returns>
    public async Task<Career> CreateCareerAsync(CareerDto careerDto)
    {   
        var career = CareerDto.ToDomain(careerDto);
        _scholarshipCalculaterService.Calculate(career);

        await _careerRepository.CreateCareerAsync(career);

        return career;
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
    /// Asynchronously retrieves all ContentType entities from the CareerRepository.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, returning a list of ContentType entities.</returns>
    public async Task<List<ContentType>> GetAllContentTypesAsync()
    {
        return await _careerRepository.GetAllContentTypesAsync();
    }

    /// <summary>
    /// Asynchronously updates a Career entity.
    /// </summary>
    /// <param name="career">The Career entity to be updated.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task UpdateCareerAsync(CareerDto careerDto)
    {
        var career = await GetCareerByIdAsync(careerDto.CareerName);
        if (career is not null)
        {
            //career = CareerDto.ToDomain(careerDto);

            var itemsToRemove = career.Contents.ToList();
            foreach (var content in itemsToRemove)
            {
                career.RemoveContent(content.Id);
            }

            var itemsToAdd = careerDto.Contents.ToArray();
            foreach(var content in itemsToAdd)
            {
                // help me here or around here, or anywhere needed.
                var newContent = ContentDto.ToDomain(content);
                career.AddContent(newContent); 
            }

            _scholarshipCalculaterService.Calculate(career);
            await _careerRepository.UpdateCareerAsync(career, true);
        } 
        else
        {
            throw new ArgumentException("Invalid Career");
        }
    }

    /// <summary>
    /// Asynchronously retrieves all Career entities.
    /// </summary>
    /// <returns> A task representing the asynchronous operation, returning a list of Career entities.</returns>
    public async Task<List<Career>> GetAllCareersAsync()
    {
        return await _careerRepository.GetAllCareersAsync();
    }
}
