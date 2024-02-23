using Microsoft.EntityFrameworkCore;
using System.Linq;
using TechnicalEvaluation.Application.Repositories;
using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Infrastructure.Repositories;

/// <summary>
/// Repository for managing Career entities in the database.
/// </summary>
public class CareerRepository : ICareerRepository
{
    private readonly ApplicationDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="CareerRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public CareerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Creates a new Career entity in the database.
    /// </summary>
    /// <param name="career">The Career entity to create.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task CreateCareerAsync(Career career)
    {
        _dbContext.Careers.Add(career);
        await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Retrieves all Career entities from the database.
    /// </summary>
    /// <returns>A list of Career entities.</returns>
    public async Task<List<Career>> GetAllCareersAsync()
    {
        return await _dbContext.Careers
            .Include(t => t.Contents)
            .Include(t => t.Areas)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves a Career entity by its unique identifier.
    /// </summary>
    /// <param name="careerName">The unique identifier of the Career entity.</param>
    /// <returns>The Career entity.</returns>
    public async Task<Career?> GetByIdAsync(CareerName careerName)
    {
        return await _dbContext.Careers
            .Include(t => t.Contents)
            .Include(c => c.Areas)
            .FirstOrDefaultAsync(t => t.Id == careerName);
    }


    /// <summary>
    /// Updates an existing Career entity in the database.
    /// </summary>
    /// <param name="career">The Career entity to update.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task UpdateCareerAsync(Career career, bool isUpdate = false)
    {
        //_dbContext.Careers.Update(career);
        //var itemsToRemove = career.Contents.ToList();
        //foreach (var content in itemsToRemove)
        //{
        //    _dbContext.Contents.Remove(content);
        //}
        //_dbContext.SaveChanges();
        if (isUpdate)
        {
            if (_dbContext.Contents is not null)
            {
                // Remove old contents
                var contentsToDelete = _dbContext.Contents.Where(c => c.Career == career).ToList();
                _dbContext.Contents.RemoveRange(contentsToDelete);

            }
            if (career.Contents is not null)
            {
                // Add new contents
                foreach (var content in career.Contents)
                {
                    if (_dbContext.Entry(content).State == EntityState.Detached)
                    {
                        if (_dbContext.Contents is not null)
                            _dbContext.Contents.Add(content);
                    }
                }
            }


            _dbContext.Update(career);

        } else
        {
            _dbContext.Careers.Update(career);
        }
        await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Searches for Career entities by name.
    /// </summary>
    /// <param name="careerName">The name to search for.</param>
    /// <returns>A list of Career entities matching the provided name.</returns>
    public async Task<List<Career>> SearchCareersByName(CareerName careerName)
    {
        return await _dbContext.Careers
        .Where(c => ((string)c.Id).Contains((string)careerName))
        .ToListAsync();
    }

    /// <summary>
    /// Asynchronously retrieves all ContentType entities from the application's DbContext.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, returning a list of ContentType entities.</returns>
    public async Task<List<ContentType>> GetAllContentTypesAsync()
    {
        return await _dbContext.ContentTypes
            .ToListAsync();
    }

    public void Update<Type>(Type obj)
    {
        _dbContext.Update(obj);
        _dbContext.SaveChanges();
    }
 }
