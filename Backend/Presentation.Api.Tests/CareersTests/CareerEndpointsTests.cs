using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using TechnicalEvaluation.Application.Careers.Dtos;
using TechnicalEvaluation.Application.UseCases;
using TechnicalEvaluation.Domain.CareerAggregate;
using TechnicalEvaluation.Presentation.Api.Careers;
using TechnicalEvaluation.Presentation.Api.Careers.Responses;

namespace TechnicalEvaluation.Presentation.Api.Tests.CareersTests;

public class CareerEndpointsTests
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
    public async Task SearchCareersByNameHandler_WhenGivenEmptyCareers_ReturnEmptyList()
    {
        // Arrange
        var careerName = "Test";
        var mockCareerUseCase = new Mock<ICareerUseCase>();
        mockCareerUseCase
            .Setup(m => m.SearchCareersByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(new List<Career>());

        // Act
        var response = await CareersEndpoints.SearchCareersByNameHandler(careerName, mockCareerUseCase.Object);

        // Assert
        response.Careers.Should().BeEmpty(
            because: 
            "The search for careers by name returned an empty list as expected based on the provided test setup."
        );

    }

    [Test]
    public async Task SearchCareersByName_WhenGivenCareers_ReturnList()
    {
        // Arrange
        var returns = new List<Career> { sampleCareer };
        var expectedResult =
            new List<CareerDto> { CareerDto.FromCareer(sampleCareer) };
        var careerName = "Test";
        var mockCareerUseCase = new Mock<ICareerUseCase>();
        mockCareerUseCase
            .Setup(m => m.SearchCareersByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(returns);

        // Act
        var response = await CareersEndpoints.SearchCareersByNameHandler(careerName, mockCareerUseCase.Object);

        // Assert
        response.Careers.Should().BeEquivalentTo(expectedResult, 
            because: "Validating that the search for careers by name yields the expected list of careers."
        );

    }

    



    [Test]
    public async Task GetByIdAsync_WhenGivenCareer_ReturnCareer()
    {
        // Arrange
        var returns = sampleCareer;
        var expectedResult = CareerDto.FromCareer(sampleCareer);
        var careerName = "Test";
        var mockCareerUseCase = new Mock<ICareerUseCase>();
        mockCareerUseCase
            .Setup(m => m.GetCareerByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(returns);


        // Act
        var response = await
            CareersEndpoints.GetCareerByIdHandler(careerName, mockCareerUseCase.Object);
        var result = response.Result as Ok<GetCareerByIdResponse>;

        // Assert
        result.Value.Career.Should().BeEquivalentTo(expectedResult, 
            because:
            "Ensures the returned career details match the expected information for a valid career retrieval by ID."
        );
    }
}
