using FluentAssertions;
using Moq;
using TechnicalEvaluation.Application.Careers.Dtos;
using TechnicalEvaluation.Application.Repositories;
using TechnicalEvaluation.Application.UseCases;
using TechnicalEvaluation.Domain.CareerAggregate;
using TechnicalEvaluation.Domain.Services;

namespace TechnicalEvaluation.Application.Tests.UseCasesTests;

public class CareerUseCaseTests
{
    [Test]
    public async Task CreateCareerAsync_ValidName_CreatesCareer()
    {
        // Arrange
        var career = new Career(
            CareerName.Create("Computacion"),
            Percentage.Create(10),
            Scholarship.Create(100)
        );
        var mockRepository = new Mock<ICareerRepository>(); 
        mockRepository
            .Setup(repository => repository.CreateCareerAsync(It.IsAny<Career>()));
        var service = new ScholarshipCalculatorService();


        var careerUseCase = new CareerUseCase(mockRepository.Object, service);

        // Act
        var result = await careerUseCase.CreateCareerAsync(CareerDto.FromCareer(career));

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(career, result);
        mockRepository.Verify(repo => repo.CreateCareerAsync(It.IsAny<Career>()), Times.Once);
        result.ScholarshipBudget.Value.Should().Be(0);
    }

    [Test]
    public async Task AddContentToCareerAsync_ValidInput_UpdatesCareer()
    {
        // Arrange
        var careerName = "Computacion";
        var contentDescription = "Cuenta con edificio anexo";
        var contentType = "Tecnologico";

        var mockCareerRepository = new Mock<ICareerRepository>();
        var career = new Career(
            CareerName.Create("Computacion"),
            Percentage.Create(10),
            Scholarship.Create(100)
        );
        var content = new Content(ContentDescription.Create(contentDescription), ContentTypeId.Create(contentType));

        mockCareerRepository
            .Setup(repo => repo.GetByIdAsync(It.IsAny<CareerName>()))
            .ReturnsAsync(career);

        mockCareerRepository
            .Setup(repo => repo.UpdateCareerAsync(It.IsAny<Career>(), It.IsAny<bool>()));

        var service = new ScholarshipCalculatorService();
        var careerUseCase = new CareerUseCase(mockCareerRepository.Object, service);

        // Act
        var result = await careerUseCase.AddContentToCareerAsync(careerName, contentDescription, contentType);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Contents.Contains(content));
        result.ScholarshipBudget.Value.Should().Be(240);
        mockCareerRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<CareerName>()), Times.Once);
        mockCareerRepository.Verify(repo => repo.UpdateCareerAsync(It.IsAny<Career>(), It.IsAny<bool>()), Times.Once);
    }

    [Test]
    public async Task AddContentToCareerAsync_WithNullDescription_ThrowException()
    {
        // Arrange
        var careerName = "Computacion";
        string contentDescription = null;
        var contentType = "Tets";
        var career = new Career(
            CareerName.Create("Computacion"),
            Percentage.Create(10),
            Scholarship.Create(100)
        );

        var mockCareerRepository = new Mock<ICareerRepository>();
        mockCareerRepository
            .Setup(repo => repo.GetByIdAsync(It.IsAny<CareerName>()))
            .ReturnsAsync(career);

        var service = new ScholarshipCalculatorService();
        var careerUseCase = new CareerUseCase(mockCareerRepository.Object, service);

        // Act
       Func<Task> result = async () =>
            await careerUseCase.AddContentToCareerAsync(careerName, contentDescription, contentType);

        // Assert
        await result.Should().ThrowAsync<ArgumentException>();
    }

    [Test]
    public async Task AddContentToCareerAsync_WithNullType_ThrowException()
    {
        // Arrange
        var careerName = "Computacion";
        var contentDescription = "Test";
        string contentType = null;
        var career = new Career(
            CareerName.Create(careerName),
            Percentage.Create(10),
            Scholarship.Create(100)
        );


        var mockCareerRepository = new Mock<ICareerRepository>();
        mockCareerRepository
            .Setup(repo => repo.GetByIdAsync(It.IsAny<CareerName>()))
            .ReturnsAsync(career);

        var service = new ScholarshipCalculatorService();
        var careerUseCase = new CareerUseCase(mockCareerRepository.Object, service);

        // Act
        Func<Task> result = async () =>
            await careerUseCase.AddContentToCareerAsync(careerName, contentDescription, contentType);

        // Assert
        await result.Should().ThrowAsync<ArgumentException>();
    }

    [Test]
    public async Task SearchCareersByNameAsync_ValidName_ReturnsMatchingCareers()
    {
        // Arrange
        var careerName = "Software Engineering";
        var mockCareerRepository = new Mock<ICareerRepository>();
        var expectedCareers = new List<Career>
        {
            new Career(
                CareerName.Create(careerName),
                Percentage.Create(10),
                Scholarship.Create(100)
            )
            // Add more Career instances if needed
        };

        mockCareerRepository
            .Setup(repo => repo.SearchCareersByName(It.IsAny<CareerName>()))
            .ReturnsAsync(expectedCareers);

        var service = new ScholarshipCalculatorService();
        var careerUseCase = new CareerUseCase(mockCareerRepository.Object, service);

        // Act
        var result = await careerUseCase.SearchCareersByNameAsync(careerName);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotEmpty(result);
        Assert.AreEqual(expectedCareers.Count, result.Count);
        foreach (var career in result)
        {
            Assert.AreEqual(careerName, career.Id.Value); // Assuming CareerName's Value is the string representation
        }
        mockCareerRepository.Verify(repo => repo.SearchCareersByName(It.IsAny<CareerName>()), Times.Once);
    }

    [Test]
    public async Task GetCareerByIdAsync_ValidId_ReturnsCareer()
    {
        // Arrange
        var careerName = "Engineering";
        var mockCareerRepository = new Mock<ICareerRepository>();
        var expectedCareer = new Career(
            CareerName.Create(careerName),
            Percentage.Create(10),
            Scholarship.Create(100)
        );

        mockCareerRepository
            .Setup(repo => repo.GetByIdAsync(It.IsAny<CareerName>()))
            .ReturnsAsync(expectedCareer);

        var service = new ScholarshipCalculatorService();
        var careerUseCase = new CareerUseCase(mockCareerRepository.Object, service);

        // Act
        var result = await careerUseCase.GetCareerByIdAsync(careerName);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(careerName, result.Id.Value); // Assuming CareerName's Value is the string representation
        mockCareerRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<CareerName>()), Times.Once);
    }

    [Test]
    public async Task GetAllContentTypesAsync_ReturnEmptyList()
    {
        // Arrange
        var mockCareerRepository = new Mock<ICareerRepository>();

        mockCareerRepository
            .Setup(repo => repo.GetAllContentTypesAsync())
            .ReturnsAsync(new List<ContentType>());

        var service = new ScholarshipCalculatorService();
        var careerUseCase = new CareerUseCase(mockCareerRepository.Object, service);

        // Act
        var result = await careerUseCase.GetAllContentTypesAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public async Task GetAllContentTypesAsync_ReturnList()
    {
        // Arrange
        var mockCareerRepository = new Mock<ICareerRepository>();
        var contentType = new ContentType(ContentTypeId.Create("Test"));
        var expectedResult = new List<ContentType> { };

        mockCareerRepository
            .Setup(repo => repo.GetAllContentTypesAsync())
            .ReturnsAsync(expectedResult);

        var service = new ScholarshipCalculatorService();
        var careerUseCase = new CareerUseCase(mockCareerRepository.Object, service);

        // Act
        var result = await careerUseCase.GetAllContentTypesAsync();

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
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
        var mockCareerRepository = new Mock<ICareerRepository>();
        var contentType = new ContentType(ContentTypeId.Create("Test"));

        mockCareerRepository
            .Setup(repo => repo.GetByIdAsync(It.IsAny<CareerName>()))
                .ReturnsAsync(career);

        mockCareerRepository
            .Setup(repo => repo.UpdateCareerAsync(It.IsAny<Career>(), It.IsAny<bool>()));

        var service = new ScholarshipCalculatorService();
        var careerUseCase = new CareerUseCase(mockCareerRepository.Object, service);


        // Act
        Func<Task> act = async() => 
            await careerUseCase.UpdateCareerAsync(CareerDto.FromCareer(career));

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Test]
    public async Task UpdateCareerAsync_ThrowException()
    {
        // Arrange
        var career = new Career(
            CareerName.Create("Test"),
            Percentage.Create(10),
            Scholarship.Create(100)
        );
        var mockCareerRepository = new Mock<ICareerRepository>();
        var contentType = new ContentType(ContentTypeId.Create("Test"));

        mockCareerRepository
            .Setup(repo => repo.GetByIdAsync(It.IsAny<CareerName>()))
                .ReturnsAsync((Career?)null);

        mockCareerRepository
            .Setup(repo => repo.UpdateCareerAsync(It.IsAny<Career>(), It.IsAny<bool>()));

        var service = new ScholarshipCalculatorService();
        var careerUseCase = new CareerUseCase(mockCareerRepository.Object, service);


        // Act
        Func<Task> act = async () =>
            await careerUseCase.UpdateCareerAsync(CareerDto.FromCareer(career));

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
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
        var mockCareerRepository = new Mock<ICareerRepository>();
        var expectedResult = new List<Career> { career };

        mockCareerRepository
            .Setup(repo => repo.GetAllCareersAsync())
            .ReturnsAsync(expectedResult);

        var service = new ScholarshipCalculatorService();
        var careerUseCase = new CareerUseCase(mockCareerRepository.Object, service);


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
        var mockCareerRepository = new Mock<ICareerRepository>();
        var expectedResult = new List<Career>();

        mockCareerRepository
            .Setup(repo => repo.GetAllCareersAsync())
            .ReturnsAsync(expectedResult);

        var service = new ScholarshipCalculatorService();
        var careerUseCase = new CareerUseCase(mockCareerRepository.Object, service);


        // Act
        var careers =
            await careerUseCase.GetAllCareersAsync();

        // Assert
        careers.Should().BeEmpty();
    }
}
