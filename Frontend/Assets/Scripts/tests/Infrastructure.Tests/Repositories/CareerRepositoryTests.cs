using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnicalEvaluation.Domain.CareerAggregate;
using TechnicalEvaluation.Infrastructure.Repositories;

namespace TechnicalEvaluation.Infrastructure.Tests.Repositories
{
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
        public async Task SearchCareersByName_WhenGivenEmptyCareers_ReturnEmptyList()
        {
            // Arrange
            var careerName = CareerName.Create("Test");
            var apiResult = new SearchCareersByNameResponse();
            apiResult.Careers = new List<CareerDto>();
            var apiClient = new Mock<ApiClient>();
            apiClient
                .Setup(m => m.CareersListGetAsync(It.IsAny<string>()))
                .ReturnsAsync(apiResult);
        
            var repository = new CareerRepository(apiClient.Object);
        
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
            var apiResult = new SearchCareersByNameResponse();
            apiResult.Careers = new List<CareerDto> { CareerDto.FromCareer(sampleCareer) };
            var apiClient = new Mock<ApiClient>();
            apiClient
                .Setup(m => m.CareersListGetAsync(It.IsAny<string>()))
                .ReturnsAsync(apiResult);

            var repository = new CareerRepository(apiClient.Object);

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
            var apiResult = new GetCareerByIdResponse();
            apiResult.Career = null;
            var apiClient = new Mock<ApiClient>();
            apiClient
                .Setup(m => m.CareersInfoAsync(It.IsAny<string>()))
                .ReturnsAsync(apiResult);

            var repository = new CareerRepository(apiClient.Object);

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
            var apiResult = new GetCareerByIdResponse();
            apiResult.Career = CareerDto.FromCareer(sampleCareer);
            var apiClient = new Mock<ApiClient>();
            apiClient
                .Setup(m => m.CareersInfoAsync(It.IsAny<string>()))
                .ReturnsAsync(apiResult);

            var repository = new CareerRepository(apiClient.Object);

            // Act
            var careers = await repository.GetByIdAsync(careerName);

            // Assert
            careers.Should().Be(sampleCareer,
                because:
                "Verifies that retrieving a career by ID returns the expected career when it exists in the repository.");
        }

        public async Task GetAllCareersAsync_HavingSampleCareer_ReturnsEqual() 
        {
            // Arrange
            var apiResult = new SearchCareersByNameResponse();
            apiResult.Careers = new List<CareerDto> { CareerDto.FromCareer(sampleCareer) };
            var apiClient = new Mock<ApiClient>();
            apiClient
                .Setup(m => m.CareersListGetAsync())
                .ReturnsAsync(new List<CareerDto>());

            var repository = new CareerRepository(apiClient.Object);

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
}


