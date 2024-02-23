using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using TechnicalEvaluation.Application.Repositories;
using TechnicalEvaluation.Domain.CareerAggregate;
using TechnicalEvaluation.Infrastructure.Repositories;

namespace TechnicalEvaluation.Infrastructure.Tests.Repositories;

public class CareerRepositoryTests
{
    private static Career sampleCareer;

    [SetUp]
    public void SetUp()
    {
        sampleCareer = new Career(
            CareerName.Create("Test"),
            Percentage.Create(10),
            Scholarship.Create(100)
        );
    }

    [Test]
    public async Task UpdateCareerAsync_Successfully()
    {
        // Arrange
        var mockContext = new Mock<ApplicationDbContext>();
        mockContext
            .Setup(m => m.Careers)
            .ReturnsDbSet(new List<Career?>());
        var repository = new CareerRepository(mockContext.Object);

        // Act
        Func<Task> action = async () => await repository.UpdateCareerAsync(sampleCareer);

        // Assert
        await action.Should().NotThrowAsync<Exception>();
    }

    [Test]
    public async Task UpdateCareerAsync_WithContent_Successfully()
    {
        // Arrange
        var mockContext = new Mock<ApplicationDbContext>();
        mockContext
            .Setup(m => m.Careers)
            .ReturnsDbSet(new List<Career?>());
        var contentDescription = ContentDescription.Create("Test");
        var contentType = ContentTypeId.Create("Test");
        var content = new Content(contentDescription, contentType);
        var repository = new CareerRepository(mockContext.Object);

        // Act
        Func<Task> action = async () => await repository.UpdateCareerAsync(sampleCareer);
        
        // Assert
        await action.Should().NotThrowAsync<Exception>();
    }



    [Test]
    public async Task SearchCareersByName_WhenGivenEmptyCareers_ReturnEmptyList()
    {
        // Arrange
        var careerName = CareerName.Create("Test");
        var mockContext = new Mock<ApplicationDbContext>();
        mockContext
            .Setup(m => m.Careers)
            .ReturnsDbSet(new List<Career?>());
        
        var repository = new CareerRepository(mockContext.Object);
        
        // Act
        var careers = await repository.SearchCareersByName(careerName);

        // Assert
        careers.Should().BeEmpty(
            because:
            "Verifies that searching for careers by name returns an empty list when no careers exist in the repository.");
    }

    [Test]
    public async Task SearchCareersByName_WhenGivenCareers_ReturnList()
    {
        // Arrange
        var expectedResult = new List<Career?> { sampleCareer };
        var careerName = CareerName.Create("Test");
        var mockContext = new Mock<ApplicationDbContext>();
        mockContext
            .Setup(m => m.Careers)
            .ReturnsDbSet(expectedResult);

        var repository = new CareerRepository(mockContext.Object);

        // Act
        var careers = await repository.SearchCareersByName(careerName);

        // Assert
        careers.Should().BeEquivalentTo(expectedResult,
            because:
            "Verifies that searching for careers by name returns a list of careers that match the given criteria.");
    }

    [Test]
    public async Task GetByIdAsync_WhenGivenNullCareer_ReturnNullCareer()
    {
        // Arrange
        var careerName = CareerName.Create("Test");
        var mockContext = new Mock<ApplicationDbContext>();
        mockContext
            .Setup(m => m.Careers)
            .ReturnsDbSet(new List<Career?>());

        var repository = new CareerRepository(mockContext.Object);

        // Act
        var careers = await repository.GetByIdAsync(careerName);

        // Assert
        careers.Should().BeNull(
            because:
            "Verifies that retrieving a career by ID returns null when the career does not exist in the repository.");
    }

    [Test]
    public async Task GetByIdAsync_WhenGivenCareer_ReturnCareer()
    {
        // Arrange
        var careerName = CareerName.Create("Test");
        var mockContext = new Mock<ApplicationDbContext>();
        mockContext
            .Setup(m => m.Careers)
            .ReturnsDbSet(new List<Career?> { sampleCareer });

        var repository = new CareerRepository(mockContext.Object);

        // Act
        var careers = await repository.GetByIdAsync(careerName);

        // Assert
        careers.Should().Be(sampleCareer,
            because:
            "Verifies that retrieving a career by ID returns the expected career when it exists in the repository.");
    }

    public async Task CreateCareerAsync_WhenGivenCareers_ReturnCareer()
    {
        // Arrange
        var mockContext = new Mock<ApplicationDbContext>();
        mockContext
            .Setup(m => m.Careers)
            .ReturnsDbSet(new List<Career?>());
        var repository = new CareerRepository(mockContext.Object);

        Exception? exception = null;

        // Act
        try
        {
            await repository.CreateCareerAsync(sampleCareer);
        }
        catch (Exception ex)
        {
            exception = ex;
        }

        // Assert
        exception.Should().BeNull(
            because: "Verifies that creating a career in the repository does not throw an exception.");
    }

    [Test]
    public async Task GetAllContentTypesAsync_ReturnEmptyList()
    {
        // Arrange
        var mockContext = new Mock<ApplicationDbContext>();
        mockContext
            .Setup(m => m.ContentTypes)
            .ReturnsDbSet(new List<ContentType>());
        var repository = new CareerRepository(mockContext.Object);

        // Act
        var result = await repository.GetAllContentTypesAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public async Task GetAllContentTypesAsync_ReturnList()
    {
        // Arrange
        var mockContext = new Mock<ApplicationDbContext>();
        var contentType = new ContentType(ContentTypeId.Create("Test"));
        var expectedResult = new List<ContentType> { contentType };
        mockContext
            .Setup(m => m.ContentTypes)
            .ReturnsDbSet(expectedResult);
        var repository = new CareerRepository(mockContext.Object);

        // Act
        var result = await repository.GetAllContentTypesAsync();

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    public async Task GetAllCareersAsync_HavingSampleCareer_ReturnsEqual() 
    {
        // Arrange
        var mockContext = new Mock<ApplicationDbContext>();
        var contentType = new ContentType(ContentTypeId.Create("Test"));
        var expecteResult = new List<ContentType> { contentType };
        mockContext
            .Setup(m => m.Careers)
            .ReturnsDbSet(new List<Career?>
            {
                sampleCareer
            });
        var repository = new CareerRepository(mockContext.Object);

        var expectedResult = new List<Career?>
        {
            sampleCareer
        };


        // Act
        var careers = await repository.GetAllCareersAsync();


        // Assert 
        careers.Should().BeEquivalentTo(expectedResult);
    }
}

