using FluentAssertions;
using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Domain.Tests.CareerAggregateTests;

public class AreaTests
{
    [Test]
    public void CreatingArea_WithDescription_Successfully()
    {

        // Arrange
        var areaDescription = AreaDescription.Create("Tecnologia");

        // Act
        var area = new Area(areaDescription);

        // Assert
        area.Id.Should().Be(areaDescription,
            because: "the area description should be assigned through the constructor");
    }

    [Test]
    public void AssignCareer_WithNullCareer_ThrowsArgumentException()
    {
        // Arrange
        var areaDescription = AreaDescription.Create("Tecnologia");
        var area = new Area(areaDescription); 

        // Act 
        Action act = () => area.AssignCareer(null);

        // Assert
        act.Should().Throw<ArgumentNullException>("because a null career cannot be assigned");
    }


    [Test]
    public void AssignCareer_WithExistingCareer_ThrowsInvalidOperationException()
    {
        // Arrange
        var areaDescription = AreaDescription.Create("Tecnologia");
        var area = new Area(areaDescription);

        var careerName = CareerName.Create("Computacion");
        var career = new Career(
            careerName,
            Percentage.Create(10),
            Scholarship.Create(100)
        );
        area.AssignCareer(career); // assign career before act.

        // Act 
        Action act = () => area.AssignCareer(career);

        // Assert
        act.Should().Throw<InvalidOperationException>("because the career is already assigned to the area");
    }

    [Test]
    public void AssignCareer_WithNewCareer_AddsSuccessfully()
    {
        // Arrange
        var areaDescription = AreaDescription.Create("Tecnologia");
        var area = new Area(areaDescription);

        var careerName = CareerName.Create("Computacion");
        var career = new Career(
            careerName,
            Percentage.Create(10),
            Scholarship.Create(100)
        );

        // Act
        area.AssignCareer(career);

        // Assert
        area.Careers.Should().Contain(career, "because the new career should be added to the area");
    }




    [Test]
    public void UnassignCareer_WithNullName_ThrowsArgumentException()
    {
        // Arrange
        var areaDescription = AreaDescription.Create("Tecnologia");
        var area = new Area(areaDescription);

        // Act 
        Action act = () => area.UnassignCareer(null);

        // Assert
        act.Should().Throw<ArgumentNullException>("because a null career name cannot be unassigned");
    }

    [Test]
    public void UnassignCareer_WithNonExistingName_ThrowsInvalidOperationException()
    {
        // Arrange
        var areaDescription = AreaDescription.Create("Tecnologia");
        var area = new Area(areaDescription);

        var careerName = CareerName.Create("Computacion");

        // Act
        Action act = () => area.UnassignCareer(careerName);

        // Assert
        act.Should().Throw<InvalidOperationException>("because the career with the given name does not belong to the area");
    }

    [Test]
    public void UnassignCareer_WithExistingName_RemovesSuccessfully()
    {
        // Arrange

        var areaDescription = AreaDescription.Create("Tecnologia");
        var area = new Area(areaDescription);

        var careerName = CareerName.Create("Computacion");
        var career = new Career(
            careerName,
            Percentage.Create(10),
            Scholarship.Create(100)
        );

        area.AssignCareer(career); // Pre-add career

        // Act
        area.UnassignCareer(careerName);

        // Assert
        area.Careers.Should().NotContain(career, "because the career should be removed from the area");
    }


}
