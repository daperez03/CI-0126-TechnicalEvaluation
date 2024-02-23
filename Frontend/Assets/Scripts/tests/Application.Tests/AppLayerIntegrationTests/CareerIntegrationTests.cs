using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnicalEvaluation.Application.Repositories;
using TechnicalEvaluation.Application.UseCases;
using TechnicalEvaluation.Domain.CareerAggregate;
using TechnicalEvaluation.Infrastructure;
using TechnicalEvaluation.Infrastructure.Repositories;

namespace TechnicalEvaluation.Application.Tests.AppLayerIntegrationTests
{
    public class CareerIntegrationTests
    {
        private Mock<ApiClient> apiClient;
        public ICareerRepository careerRepository;
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

            apiClient = new Mock<ApiClient>();
            careerRepository = new CareerRepository(apiClient.Object);
            careerUseCase = new CareerUseCase(careerRepository);
        }

        private List<CareerDto> ToCareerDtos(List<Career> careers)
        {
            var careerDtos = new List<CareerDto>();
            foreach (var career in careers)
            {
                careerDtos.Add(CareerDto.FromCareer(career));
            }
            return careerDtos;
        }

        [Test]
        public async Task SearchCareersByNameAsync_ReturnCareers_Successfully()
        {
            // Arrange
            var expectedResult = new List<Career> { sampleCareer1, sampleCareer2 };
            var apiResult = new SearchCareersByNameResponse();
            apiResult.Careers = ToCareerDtos(expectedResult);
            apiClient.Setup(c => c.CareersListGetAsync(It.IsAny<string>()))
                .ReturnsAsync(apiResult);

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
            var apiResult = new SearchCareersByNameResponse();
            apiResult.Careers = ToCareerDtos(expectedResult);
            apiClient.Setup(c => c.CareersListGetAsync(It.IsAny<string>()))
                .ReturnsAsync(apiResult);

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
            var apiResult = new SearchCareersByNameResponse();
            apiResult.Careers = ToCareerDtos(expectedResult);
            apiClient.Setup(c => c.CareersListGetAsync(It.IsAny<string>()))
                .ReturnsAsync(apiResult);

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
            var apiResult = new GetCareerByIdResponse();
            apiResult.Career = CareerDto.FromCareer(sampleCareer1);
            apiClient.Setup(c => c.CareersInfoAsync(It.IsAny<string>()))
                .ReturnsAsync(apiResult);

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
            var apiResult = new GetCareerByIdResponse();
            apiResult.Career = CareerDto.FromCareer(sampleCareer1);
            apiClient.Setup(c => c.CareersInfoAsync(It.IsAny<string>()))
                .ReturnsAsync(apiResult);

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
            var apiResult = new GetCareerByIdResponse();
            apiResult.Career = CareerDto.FromCareer(sampleCareer1);
            apiClient.Setup(c => c.CareersInfoAsync(It.IsAny<string>()))
                .ReturnsAsync(apiResult);

            string careerName = "1234567890123456789012345678901";

            // Act
            Func<Task> result = async () =>
                await careerUseCase.GetCareerByIdAsync(careerName);

            // Assert
            await result.Should().ThrowAsync<ArgumentException>();
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

            apiClient.Setup(c => c.CareersListGetAsync())
                .ReturnsAsync(ToCareerDtos(expectedResult));

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

            apiClient.Setup(c => c.CareersListGetAsync())
                .ReturnsAsync(ToCareerDtos(expectedResult));

            // Act
            var careers =
                await careerUseCase.GetAllCareersAsync();

            // Assert
            careers.Should().BeEmpty();
        }
    }
}

