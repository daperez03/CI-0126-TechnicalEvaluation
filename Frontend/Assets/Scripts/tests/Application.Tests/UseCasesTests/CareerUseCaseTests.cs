using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnicalEvaluation.Application.Repositories;
using TechnicalEvaluation.Application.UseCases;
using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Application.Tests.UseCasesTests
{
    public class CareerUseCaseTests
    {
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

            var careerUseCase = new CareerUseCase(mockCareerRepository.Object);

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

            var careerUseCase = new CareerUseCase(mockCareerRepository.Object);

            // Act
            var result = await careerUseCase.GetCareerByIdAsync(careerName);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(careerName, result.Id.Value); // Assuming CareerName's Value is the string representation
            mockCareerRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<CareerName>()), Times.Once);
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

            var careerUseCase = new CareerUseCase(mockCareerRepository.Object);


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

            var careerUseCase = new CareerUseCase(mockCareerRepository.Object);


            // Act
            var careers =
                await careerUseCase.GetAllCareersAsync();

            // Assert
            careers.Should().BeEmpty();
        }
    }
}

