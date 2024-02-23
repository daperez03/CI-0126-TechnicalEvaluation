using FluentAssertions;
using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Domain.Tests.CareerAggregateTests;

public class CareerTests
{
    [Test]
    public void CreatingCareer_AssignsName_Successfully()
    {
        // Arrange
        var careerName = CareerName.Create("Filosofia");

        // Act
        var career = new Career(
            careerName,
            Percentage.Create(10),
            Scholarship.Create(100)
        );

        // Assert
        career.Id.Should().Be(careerName,
            because: "the career name should be assigned through the constructor");
    }

    [Test]
    public void ComparingCareers_WithSameId_ReturnsEqual()
    {
        // Arrange
        var careerName = CareerName.Create("Filosofia");

        // Act
        var career1 = new Career(
            careerName,
            Percentage.Create(10),
            Scholarship.Create(100)
        );
        var career2 = new Career(
            careerName,
            Percentage.Create(10),
            Scholarship.Create(100)
        );

        // Assert
        career1.Should().Be(career2,
            because: "careers with equal ID's should be equal");
    }

    [Test]
    public void ComparingCareers_WithDifferentIds_ReturnsNotEqual()
    {
        // Arrange
        var careerName1 = CareerName.Create("Filosofia");
        var careerName2 = CareerName.Create("Computacion");


        // Act
        var career1 = new Career(
            careerName1,
            Percentage.Create(10),
            Scholarship.Create(100)
        );
        var career2 = new Career(
            careerName2,
            Percentage.Create(10),
            Scholarship.Create(100)
        );

        // Assert
        career1.Should().NotBe(career2,
            because: "careers with different ID's shouldn't be equal");
    }

    [Test]
    public void AddingContentToCareer_ThatDoesNotExist_ExecutesSuccessfully()
    {
        // Arrange
        var career = new Career(
            CareerName.Create("Filosofia"),
            Percentage.Create(10),
            Scholarship.Create(100)
        );
        var contentDescription = ContentDescription.Create("Creada en 1850");
        var contentType = ContentTypeId.Create("Tets");
        var content = new Content(contentDescription, contentType);

        // Act
        Action act = () => career.AddContent(content);

        // Assert
        act.Should().NotThrow<InvalidOperationException>(
            because: "adding non existing content should not throw an exception");
    }

    [Test]
    public void AddingContentToCareer_ThatAlreadyExists_ThrowsException()
    {
        // Arrange
        var career = new Career(
            CareerName.Create("Filosofia"),
            Percentage.Create(10),
            Scholarship.Create(100)
        );
        var contentDescription = ContentDescription.Create("Creada en 1850");
        var contentType = ContentTypeId.Create("Tets");
        var content = new Content(contentDescription, contentType);


        career.AddContent(content);

        // Act
        Action act = () => career.AddContent(content);

        // Assert
        act.Should().Throw<InvalidOperationException>(
            because: "adding already existing content should throw an exception");
    }


}
