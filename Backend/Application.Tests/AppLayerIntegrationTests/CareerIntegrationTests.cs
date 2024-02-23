using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using TechnicalEvaluation.Application.Careers.Dtos;
using TechnicalEvaluation.Application.Repositories;
using TechnicalEvaluation.Application.UseCases;
using TechnicalEvaluation.Domain.CareerAggregate;
using TechnicalEvaluation.Domain.Services;
using TechnicalEvaluation.Infrastructure;
using TechnicalEvaluation.Infrastructure.Repositories;

namespace TechnicalEvaluation.Application.Tests.AppLayerIntegrationTests;

public class CareerIntegrationTests
{
    private Mock<ApplicationDbContext> dbContextMock;
    public ICareerRepository careerRepository;
    public IScholarshipCalculatorService service;
    public ICareerUseCase careerUseCase;

    private static Career sampleCareer1;
    private static Career sampleCareer2;
    private static Content sampleContent1;
    private static Content sampleContent2;

    [SetUp]
    public void CareerSetUp()
    {
        sampleCareer1 = new Career(
            CareerName.Create("Computacion"),
            Percentage.Create(10),
            Scholarship.Create(100)
        );
        sampleCareer2 = new Career(
            CareerName.Create("Computacion"),
            Percentage.Create(10),
            Scholarship.Create(100)
        );
        sampleContent1 = new Content(ContentDescription.Create("Test"), ContentTypeId.Create("Tecnologico"));
        sampleContent2 = new Content(ContentDescription.Create("Test"), ContentTypeId.Create("Ambiental"));
        // Set up the DbContext mock for the repository
        dbContextMock = new Mock<ApplicationDbContext>();
        careerRepository = new CareerRepository(dbContextMock.Object);
        service = new ScholarshipCalculatorService();
        careerUseCase = new CareerUseCase(careerRepository, service);
    }

    [Test]
    public async Task CreateCareerAsync_ReturnCareer_Successfully()
    {
        // Arrange
        dbContextMock.Setup(c => c.Careers)
            .ReturnsDbSet(new List<Career> { sampleCareer1 } );

        // Act
        var result = await careerUseCase.CreateCareerAsync(CareerDto.FromCareer(sampleCareer1));

        // Assert
        result.ScholarshipBudget.Value.Should().Be(0);
        result.Should().Be(sampleCareer1);
    }

    [Test]
    public async Task CreateCareerAsync_With31Chars_ThrowException()
    {
        // Arrange
        dbContextMock.Setup(c => c.Careers)
            .ReturnsDbSet(new List<Career>());
        string careerName = "1234567890123456789012345678901";


        // Act
        Func<Task> result = 
            async () => await careerUseCase.CreateCareerAsync(
                new CareerDto(
                    careerName,
                    10,
                    1000,
                    new List<ContentDto>(),
                    new List<AreaDto>()
                )
            );

        // Assert
        await result.Should().ThrowAsync<ArgumentException>();
    }

    [Test]
    public async Task AddContentToCareerAsync_ReturnCareer_Successfully()
    {
        // Arrange
        dbContextMock.Setup(c => c.Careers)
            .ReturnsDbSet(new List<Career> { sampleCareer1 });

        // Act
        var result = 
            await careerUseCase.AddContentToCareerAsync(sampleCareer1.Id.Value,
            sampleContent1.Id.Value, sampleContent1.ContentType.Value);

        // Assert
        result.ScholarshipBudget.Value.Should().Be(240);
        result.Contents.Should().BeEquivalentTo(new List<Content> { sampleContent1 });
    }

    [Test]
    public async Task AddContentToCareerAsync_NullCareerName_ThrowException()
    {
        // Arrange
        dbContextMock.Setup(c => c.Careers)
            .ReturnsDbSet(new List<Career>());

        // Act
        Func<Task> result = async () =>
            await careerUseCase.AddContentToCareerAsync(null, "Test", "Test");

        // Asser
        await result.Should().ThrowAsync<ArgumentException>();
    }

    [Test]
    public async Task AddContentToCareerAsync_CareerName_With31Chars_ThrowException()
    {
        // Arrange
        dbContextMock.Setup(c => c.Careers)
            .ReturnsDbSet(new List<Career>());
        string careerName = "1234567890123456789012345678901";

        // Act
        Func<Task> result = async () =>
            await careerUseCase.AddContentToCareerAsync(careerName, "Test", "Test");

        // Assert
        await result.Should().ThrowAsync<ArgumentException>();
    }

    [Test]
    public async Task AddContentToCareerAsync_NullContentDescription_ThrowException()
    {
        // Arrange
        dbContextMock.Setup(c => c.Careers)
            .ReturnsDbSet(new List<Career> { sampleCareer1 });

        // Act
        Func<Task> result = async () =>
            await careerUseCase.AddContentToCareerAsync(sampleCareer1.Id.Value, null, "Test");

        // Assert
        await result.Should().ThrowAsync<ArgumentException>();
    }

    [Test]
    public async Task AddContentToCareerAsync_ContentDescription_With256Chars_ThrowException()
    {
        // Arrange
        dbContextMock.Setup(c => c.Careers)
            .ReturnsDbSet(new List<Career> { sampleCareer1 });
        string contentDescription = "";
        for (var i = 0; i < 256; i++) contentDescription += 'a';

        // Act
        Func<Task> result = async () =>
            await careerUseCase.AddContentToCareerAsync(sampleCareer1.Id.Value, contentDescription, "Test");

        // Assert
        await result.Should().ThrowAsync<ArgumentException>();
    }

    [Test]
    public async Task AddContentToCareerAsync_NullContentType_ThrowException()
    {
        // Arrange
        dbContextMock.Setup(c => c.Careers)
            .ReturnsDbSet(new List<Career> { sampleCareer1 });

        // Act
        Func<Task> result = async () =>
            await careerUseCase.AddContentToCareerAsync(sampleCareer1.Id.Value, "Test", null);


        // Assert
        await result.Should().ThrowAsync<ArgumentException>();
    }

    [Test]
    public async Task AddContentToCareerAsync_ContentDescription_With31Chars_ThrowException()
    {
        // Arrange
        dbContextMock.Setup(c => c.Careers)
            .ReturnsDbSet(new List<Career> { sampleCareer1 });
        string contentType = "1234567890123456789012345678901";

        // Act
        Func<Task> result = async () =>
            await careerUseCase.AddContentToCareerAsync(sampleCareer1.Id.Value, "Test", contentType);

        // Assert
        await result.Should().ThrowAsync<ArgumentException>();
    }

    [Test]
    public async Task SearchCareersByNameAsync_ReturnCareers_Successfully()
    {
        // Arrange
        var expectedResult = new List<Career> { sampleCareer1, sampleCareer2 };
        dbContextMock.Setup(c => c.Careers)
            .ReturnsDbSet(expectedResult);

        // Act
        var result = 
            await careerUseCase.SearchCareersByNameAsync(sampleCareer1.Id.Value);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public async Task SearchCareersByNameAsync_WithNullName_ThrowException()
    {
        // Arrange
        var expectedResult = new List<Career> { sampleCareer1, sampleCareer2 };
        dbContextMock.Setup(c => c.Careers)
            .ReturnsDbSet(expectedResult);

        // Act
        Func<Task> result = async () =>
            await careerUseCase.SearchCareersByNameAsync(null);

        // Assert
        await result.Should().ThrowAsync<ArgumentException>();
    }

    [Test]
    public async Task SearchCareersByNameAsync_With31Chars_ThrowException()
    {
        // Arrange
        var expectedResult = new List<Career> { sampleCareer1, sampleCareer2 };
        dbContextMock.Setup(c => c.Careers)
            .ReturnsDbSet(expectedResult);
        string careerName = "1234567890123456789012345678901";

        // Act
        Func<Task> result = async () =>
            await careerUseCase.SearchCareersByNameAsync(careerName);

        // Assert
        await result.Should().ThrowAsync<ArgumentException>();
    }

    [Test]
    public async Task GetCareerByIdAsync_ReturnCareer_Successfully()
    {
        // Arrange
        var expectedResult = new List<Career> { sampleCareer1 };
        dbContextMock.Setup(c => c.Careers)
            .ReturnsDbSet(expectedResult);

        // Act
        var result =
            await careerUseCase.GetCareerByIdAsync(sampleCareer1.Id.Value);

        // Assert
        result.Should().Be(sampleCareer1);
    }

    [Test]
    public async Task GetCareerByIdAsync_WithNullName_ThrowException()
    {
        // Arrange
        var expectedResult = new List<Career> { sampleCareer1, sampleCareer2 };
        dbContextMock.Setup(c => c.Careers)
            .ReturnsDbSet(expectedResult);

        // Act
        Func<Task> result = async () =>
            await careerUseCase.GetCareerByIdAsync(null);

        // Assert
        await result.Should().ThrowAsync<ArgumentException>();
    }

    [Test]
    public async Task GetCareerByIdAsync_With31Chars_ThrowException()
    {
        // Arrange
        var expectedResult = new List<Career> { sampleCareer1, sampleCareer2 };
        dbContextMock.Setup(c => c.Careers)
            .ReturnsDbSet(expectedResult);
        string careerName = "1234567890123456789012345678901";

        // Act
        Func<Task> result = async () =>
            await careerUseCase.GetCareerByIdAsync(careerName);

        // Assert
        await result.Should().ThrowAsync<ArgumentException>();
    }

    [Test]
    public async Task GetAllContentTypesAsync_ReturnList()
    {
        // Arrange
        var contentType = new ContentType(ContentTypeId.Create("Test"));
        var expectedResult = new List<ContentType> { contentType };
        dbContextMock.Setup(c => c.ContentTypes)
            .ReturnsDbSet(expectedResult);

        // Act
        var result = 
            await careerUseCase.GetAllContentTypesAsync();

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public async Task GetAllContentTypesAsync_ReturnEmptyList()
    {
        // Arrange
        var expectedResult = new List<ContentType>();
        dbContextMock.Setup(c => c.ContentTypes)
            .ReturnsDbSet(expectedResult);

        // Act
        var result =
            await careerUseCase.GetAllContentTypesAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public async Task UpdateCareerAsync_Succesfully()
    {
        // Arrange
        var career = new Career(
            CareerName.Create("Test"),
            Percentage.Create(10),
            Scholarship.Create(100)
        );
        dbContextMock.Setup(c => c.Careers)
            .ReturnsDbSet(new List<Career> { career });
        
        // Act
        Func<Task> act = async () =>
            await careerUseCase.UpdateCareerAsync(CareerDto.FromCareer(career));

        // Assert
        await act
            .Should().NotThrowAsync();
    }

    [Test]
    public async Task GetAllCareersAsync_ReturnListOfCareers()
    {
        // Arrange
        var career = new Career(
            CareerName.Create("Test"),
            Percentage.Create(10),
            Scholarship.Create(100)
        );
        var expectedResult = new List<Career> { career };

        dbContextMock.Setup(c => c.Careers)
            .ReturnsDbSet(expectedResult);

        // Act
        var careers =
            await careerUseCase.GetAllCareersAsync();

        // Assert
        careers.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public async Task GetAllCareersAsync_ReturnEmptyList()
    {
        // Arrange
        var expectedResult = new List<Career>();

        dbContextMock.Setup(c => c.Careers)
            .ReturnsDbSet(expectedResult);

        // Act
        var careers =
            await careerUseCase.GetAllCareersAsync();

        // Assert
        careers.Should().BeEmpty();
    }
}
