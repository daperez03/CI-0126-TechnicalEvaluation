using FluentAssertions;
using TechnicalEvaluation.Domain.CareerAggregate;
using TechnicalEvaluation.Infrastructure.Repositories;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnicalEvaluation.Infrastructure;
using System.Net.Http;
using System;

namespace TechnicalEvaluation.Infrastructure.Tests.IntegrationTests
{
    public class CareerIntegrationTests
    {
        private static ApiClient _client = new ApiClient("https://localhost:7245", new HttpClient());
        CareerRepository _repository = new CareerRepository(_client);

        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public async Task GetAllCareersAsync_Succesfully()
        {
            // Act
            var testName = CareerName.Create("Comp");

            Func<Task> act = async() => await _repository.GetAllCareersAsync();

            // Assert 
            await act.Should().NotThrowAsync();
        }

        [Test]
        public async Task SearchCareersByName_Succesfully()
        {
            // Arrange
            var careerName = CareerName.Create("Test");

            // Act
            Func<Task> act = async () => await _repository.SearchCareersByName(careerName);

            // Assert
            await act.Should().NotThrowAsync();
        }

        [Test]
        public async Task GetByIdAsync_Succesfully()
        {
            // Arrange
            var careerName = CareerName.Create("Test");

            // Act
            Func<Task> act = async () => await _repository.GetByIdAsync(careerName);

            // Assert
            await act.Should().NotThrowAsync();
        }
    }
}

