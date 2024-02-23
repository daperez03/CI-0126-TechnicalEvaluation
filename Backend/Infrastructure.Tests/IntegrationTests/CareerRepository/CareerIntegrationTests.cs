using FluentAssertions;
using TechnicalEvaluation.Domain.CareerAggregate;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;
using TechnicalEvaluation.Infrastructure.Tests.IntegrationTests;
using TechnicalEvaluation.Infrastructure;
using Microsoft.EntityFrameworkCore;
using TechnicalEvaluation.Infrastructure.Repositories;
using Moq;
using Moq.EntityFrameworkCore;

namespace TechnicalEvaluation.Domain.Tests.CareerAggregateTests;

public class CareerIntegrationTests
{
    private static string sqlScript = DatabaseSetup.GetScript("PostDeploymentCareerAggregate.sql");

    private static Career careerToAdd;
    private static Career sampleCareer;
    private static Career existingCareer1;
    private static Career existingCareer2;
    private static Career existingCareer3;
    private static ContentType existingContentType1;
    private static ContentType existingContentType2;
    private static ContentType existingContentType3;
    private static ContentType existingContentType4;


    public static ApplicationDbContext CreateContext()
        => new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(IntegrationTestSettings.connectionString)
                .Options);

    [SetUp]
    public void SetUp()
    {
        DatabaseSetup.TestDatabaseFixture(sqlScript);

        // Create dummy careers for testing
        var careerNameToAdd = CareerName.Create("Arte");
        careerToAdd = new Career(
            careerNameToAdd,
            Percentage.Create(10),
            Scholarship.Create(100)
        );

        sampleCareer = new Career(
            CareerName.Create("Test"),
            Percentage.Create(10),
            Scholarship.Create(100)
        );

        // Create already database-existing careers
        var careerName1 = CareerName.Create("Computacion");
        existingCareer1 = new Career(
            careerName1,
            Percentage.Create(10),
            Scholarship.Create(100)
        );

        var careerName2 = CareerName.Create("Derecho");
        existingCareer2 = new Career(
            careerName2,
            Percentage.Create(10),
            Scholarship.Create(100)
        );
        var careerName3 = CareerName.Create("Matematica");
        existingCareer3 = new Career(
            careerName3,
            Percentage.Create(10),
            Scholarship.Create(100)
        );

        existingContentType1 = new ContentType(ContentTypeId.Create("Matematicas"));
        existingContentType2 = new ContentType(ContentTypeId.Create("Ciencias"));
        existingContentType3 = new ContentType(ContentTypeId.Create("Tecnologia"));
        existingContentType4 = new ContentType(ContentTypeId.Create("Historia"));
    }

    [Test]
    public async Task CreateCareerAsync_GivenCareer_SuccessfullyAsync()
    {
        // Arrange
        DatabaseSetup.TestDatabaseFixture(sqlScript);
        var careerRepository = new CareerRepository(CreateContext());


        // Act
        Func<Task> act = async() =>
            await careerRepository.CreateCareerAsync(sampleCareer);

        // Assert
        await act.Should().NotThrowAsync();
    }


    [Test]
    public async Task GetAllCareersAsync_HavingSampleCareer_ReturnsEqual()
    {
        // Arrange
        DatabaseSetup.TestDatabaseFixture(sqlScript);
        var repository = new CareerRepository(CreateContext());

        var expectedResult = new List<Career?>
        {
            existingCareer1,
            existingCareer2,
            existingCareer3
        };

        // Act
        var testName = CareerName.Create("Comp");

        var careers = await repository.GetAllCareersAsync();

        // Assert 
        careers.Should().BeEquivalentTo(expectedResult);
    }



    [Test]
    public async Task SearchCareersByName_WhenGivenNonMatchingCareerName_ReturnsEmptyList()
    {
        // Arrange
        var careerName = CareerName.Create("Test");
        DatabaseSetup.TestDatabaseFixture(sqlScript);
        var careerRepository = new CareerRepository(CreateContext());

        // Act
        var careers = await careerRepository.SearchCareersByName(careerName);

        // Assert
        careers.Should().BeEmpty();
    }

    [Test]
    public async Task SearchCareersByName_WhenGivenMatchingCareerName_ReturnsMatchingList()
    {
        // Arrange
        var expectedResult = new List<Career> { existingCareer1 };
        var careerName = CareerName.Create("Comp");
        DatabaseSetup.TestDatabaseFixture(sqlScript);
        var careerRepository = new CareerRepository(CreateContext());

        // Act
        var careers = await careerRepository.SearchCareersByName(careerName);

        // Assert
        careers.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public async Task GetByIdAsync_WhenGivenNullCareer_ReturnNullCareer()
    {
        // Arrange
        var careerName = CareerName.Create("Test");
        DatabaseSetup.TestDatabaseFixture(sqlScript);
        var careerRepository = new CareerRepository(CreateContext());

        // Act
        var careers = await careerRepository.GetByIdAsync(careerName);

        // Assert
        careers.Should().BeNull();
    }

    [Test]
    public async Task GetByIdAsync_WhenGivenCareer_ReturnCareer()
    {
        // Arrange
        var careerName = CareerName.Create("Computacion");
        DatabaseSetup.TestDatabaseFixture(sqlScript);
        var careerRepository = new CareerRepository(CreateContext());

        // Act
        var careers = await careerRepository.GetByIdAsync(careerName);

        // Assert
        careers.Should().Be(existingCareer1);
    }

    [Test]
    public async Task GetAllContentTypesAsync_ReturnList()
    {
        // Arrange
        DatabaseSetup.TestDatabaseFixture(sqlScript);
        var careerRepository = new CareerRepository(CreateContext());
        var expectedResult = new List<ContentType> { 
            existingContentType1,
            existingContentType2,
            existingContentType3,
            existingContentType4
        };

        // Act
        var result = await careerRepository.GetAllContentTypesAsync();

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
}
