using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalEvaluation.Application.Careers.Dtos;
using TechnicalEvaluation.Application.Repositories;
using TechnicalEvaluation.Application.UseCases;
using TechnicalEvaluation.Domain.CareerAggregate;
using TechnicalEvaluation.Domain.Services;
using TechnicalEvaluation.Infrastructure;
using TechnicalEvaluation.Infrastructure.Repositories;

namespace TechnicalEvaluation.Application.Tests.AppLayerIntegrationTests;

public class ScholarshipServiceIntegrationTests
{
    private Mock<ICareerRepository> careerRepository;
    private IScholarshipCalculatorService service;
    private ICareerUseCase careerUseCase;
    private Dictionary<string, ContentDto> contentDtos = new Dictionary<string, ContentDto>();
    private Dictionary<string, AreaDto> areaDtos = new Dictionary<string, AreaDto>();

    [SetUp]
    public void SetUp()
    {
        // Set up the DbContext mock for the repository
        careerRepository = new Mock<ICareerRepository>();
        service = new ScholarshipCalculatorService();
        careerUseCase = new CareerUseCase(careerRepository.Object, service);
        var description = ContentDescription.Create("Tests");
        contentDtos.Clear();
        contentDtos.Add("Tecnologico", new ContentDto("Tests1", "Tecnologico"));
        contentDtos.Add("Ambiental", new ContentDto("Tests2", "Ambiental"));
        contentDtos.Add("Social", new ContentDto("Tests3", "Social"));
        areaDtos.Clear();
        areaDtos.Add("Ciencia", new AreaDto("Ciencia"));
        areaDtos.Add("Tecnologia", new AreaDto("Tecnologia"));
        areaDtos.Add("Ingenieria", new AreaDto("Ingenieria"));
        areaDtos.Add("Matematica", new AreaDto("Matematica"));
        areaDtos.Add("Computacion e Informatica", new AreaDto("Computacion e Informatica"));
    }

    [Test]
    public async Task CreateCareer_With_EmptyContentsAndAreas_ReturnCalculation()
    {
        // Arrange
        var careerDto = new CareerDto(
            "Test",
            40,
            10,
            new List<ContentDto>(),
            new List<AreaDto>());
        careerRepository.Setup(r => r.CreateCareerAsync(It.IsAny<Career>()));
        
        // Act
        var career = await careerUseCase.CreateCareerAsync(careerDto);

        // Assert
        career.ScholarshipBudget.Value.Should().Be(0);
    }

    [Test]
    public async Task CreateCareer_With_AllContentsAndEmptyAreas_ReturnCalculation()
    {
        // Arrange
        var careerDto = new CareerDto(
            "Test",
            40,
            10,
            contentDtos.Values.ToList(),
            new List<AreaDto>());
        careerRepository.Setup(r => r.CreateCareerAsync(It.IsAny<Career>()));

        // Act
        var career = await careerUseCase.CreateCareerAsync(careerDto);

        // Assert
        career.ScholarshipBudget.Value.Should().Be(480);
    }

    [Test]
    public async Task CreateCareer_With_EmptyContentsAndAllAreas_ReturnCalculation()
    {
        // Arrange
        var careerDto = new CareerDto(
            "Test",
            40,
            10,
            new List<ContentDto>(),
            areaDtos.Values.ToList());
        careerRepository.Setup(r => r.CreateCareerAsync(It.IsAny<Career>()));

        // Act
        var career = await careerUseCase.CreateCareerAsync(careerDto);

        // Assert
        career.ScholarshipBudget.Value.Should().Be(0);
    }

    [Test]
    public async Task CreateCareer_With_AllContentsAndAreas_ReturnCalculation()
    {
        // Arrange
        var careerDto = new CareerDto(
            "Test",
            40,
            10,
            contentDtos.Values.ToList(),
            areaDtos.Values.ToList());
        careerRepository.Setup(r => r.CreateCareerAsync(It.IsAny<Career>()));

        // Act
        var career = await careerUseCase.CreateCareerAsync(careerDto);

        // Assert
        career.ScholarshipBudget.Value.Should().Be(631);
    }

    [Test]
    public async Task CreateCareer_With_60OfWomenPercentage_EmptyContentsAndAreas_ReturnCalculation()
    {
        // Arrange
        var careerDto = new CareerDto(
            "Test",
            60,
            10,
            new List<ContentDto>(),
            new List<AreaDto>());
        careerRepository.Setup(r => r.CreateCareerAsync(It.IsAny<Career>()));

        // Act
        var career = await careerUseCase.CreateCareerAsync(careerDto);

        // Assert
        career.ScholarshipBudget.Value.Should().Be(0);
    }

    [Test]
    public async Task CreateCareer_With_60OfWomenPercentage_AllContentsAndAreas_ReturnCalculation()
    {
        // Arrange
        var careerDto = new CareerDto(
            "Test",
            60,
            10,
            contentDtos.Values.ToList(),
            areaDtos.Values.ToList());
        careerRepository.Setup(r => r.CreateCareerAsync(It.IsAny<Career>()));

        // Act
        var career = await careerUseCase.CreateCareerAsync(careerDto);

        // Assert
        career.ScholarshipBudget.Value.Should().Be(694.84);
    }

    [Test]
    public async Task UpdateCareer_AllContentsAndAreas_Successfully()
    {
        // Arrange
        var careerDto = new CareerDto(
            "Test",
            40,
            10,
            contentDtos.Values.ToList(),
            areaDtos.Values.ToList());
        careerRepository.Setup(r => r.UpdateCareerAsync(It.IsAny<Career>(), It.IsAny<bool>()));
        careerRepository.Setup(r => r.GetByIdAsync(It.IsAny<CareerName>()))
            .ReturnsAsync(CareerDto.ToDomain(careerDto));

        // Act
        Func<Task> act = 
            async () => await careerUseCase.UpdateCareerAsync(careerDto);

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Test]
    public async Task UpdateCareer_EmptyContentsAndAreas_Successfully()
    {
        // Arrange
        var careerDto = new CareerDto(
            "Test",
            40,
            10,
            new List<ContentDto>(),
            new List<AreaDto>());
        careerRepository.Setup(r => r.UpdateCareerAsync(It.IsAny<Career>(), It.IsAny<bool>()));
        careerRepository.Setup(r => r.GetByIdAsync(It.IsAny<CareerName>()))
            .ReturnsAsync(CareerDto.ToDomain(careerDto));

        // Act
        Func<Task> act =
            async () => await careerUseCase.UpdateCareerAsync(careerDto);

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Test]
    public async Task UpdateCareer_AllContentsAndEmptyAreas_Successfully()
    {
        // Arrange
        var careerDto = new CareerDto(
            "Test",
            40,
            10,
            contentDtos.Values.ToList(),
            new List<AreaDto>());
        careerRepository.Setup(r => r.UpdateCareerAsync(It.IsAny<Career>(), It.IsAny<bool>()));
        careerRepository.Setup(r => r.GetByIdAsync(It.IsAny<CareerName>()))
            .ReturnsAsync(CareerDto.ToDomain(careerDto));

        // Act
        Func<Task> act =
            async () => await careerUseCase.UpdateCareerAsync(careerDto);

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Test]
    public async Task UpdateCareer_EmptyContentsAndAllAreas_Successfully()
    {
        // Arrange
        var careerDto = new CareerDto(
            "Test",
            40,
            10,
            new List<ContentDto>(),
            areaDtos.Values.ToList());
        careerRepository.Setup(r => r.UpdateCareerAsync(It.IsAny<Career>(), It.IsAny<bool>()));
        careerRepository.Setup(r => r.GetByIdAsync(It.IsAny<CareerName>()))
            .ReturnsAsync(CareerDto.ToDomain(careerDto));

        // Act
        Func<Task> act =
            async () => await careerUseCase.UpdateCareerAsync(careerDto);

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Test]
    public async Task UpdateCareer_With_60OfWomenPercentage_EmptyContentsAndAreas_Successfully()
    {
        // Arrange
        var careerDto = new CareerDto(
            "Test",
            60,
            10,
            new List<ContentDto>(),
            new List<AreaDto>());
        careerRepository.Setup(r => r.UpdateCareerAsync(It.IsAny<Career>(), It.IsAny<bool>()));
        careerRepository.Setup(r => r.GetByIdAsync(It.IsAny<CareerName>()))
            .ReturnsAsync(CareerDto.ToDomain(careerDto));

        // Act
        Func<Task> act =
            async () => await careerUseCase.UpdateCareerAsync(careerDto);

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Test]
    public async Task UpdateCareer_With_60OfWomenPercentage_AllContentsAndAreas_Successfully()
    {
        // Arrange
        var careerDto = new CareerDto(
            "Test",
            60,
            10,
            contentDtos.Values.ToList(),
            areaDtos.Values.ToList());
        careerRepository.Setup(r => r.UpdateCareerAsync(It.IsAny<Career>(), It.IsAny<bool>()));
        careerRepository.Setup(r => r.GetByIdAsync(It.IsAny<CareerName>()))
            .ReturnsAsync(CareerDto.ToDomain(careerDto));

        // Act
        Func<Task> act =
            async () => await careerUseCase.UpdateCareerAsync(careerDto);

        // Assert
        await act.Should().NotThrowAsync();
    }
}
