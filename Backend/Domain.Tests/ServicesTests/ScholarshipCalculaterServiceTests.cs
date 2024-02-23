using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalEvaluation.Domain.CareerAggregate;
using TechnicalEvaluation.Domain.Services;

namespace TechnicalEvaluation.Domain.Tests.ServicesTests;

public class ScholarshipCalculaterServiceTests
{
    [Test]
    public void CalculateScholarship_WhitoutBudget()
    {

        // Arrange
        var service = new ScholarshipCalculatorService();
        var career = new Career(
                CareerName.Create("Test"),
                Percentage.Create(40),
                Scholarship.Create(100000)
            );

        // Act
        service.Calculate(career );

        // Assert
        career.ScholarshipBudget.Value.Should().Be(0);
    }

    [Test]

    public void CalculateScholarship_WhitContent()
    {

        // Arrange
        var service = new ScholarshipCalculatorService();
        var career = new Career(
                CareerName.Create("Test"),
                Percentage.Create(40),
                Scholarship.Create(100000)
            );
        career.AddContent(new Content(
            ContentDescription.Create("Test"),
            ContentTypeId.Create("Tecnologico"))
        );

        // Act
        service.Calculate(career);

        // Assert
        career.ScholarshipBudget.Value.Should().Be(240);
    }

    [Test]
    public void CalculateScholarship_WhitSTEMArea()
    {

        // Arrange
        var service = new ScholarshipCalculatorService();
        var career = new Career(
                CareerName.Create("Test"),
                Percentage.Create(40),
                Scholarship.Create(100000)
            );
        career.AddArea(new Area(
            AreaDescription.Create("Tecnologia"))
        );

        // Act
        service.Calculate(career);

        // Assert
        career.ScholarshipBudget.Value.Should().Be(0);
    }

    [Test]
    public void CalculateScholarship_WhitContentAndArea()
    {

        // Arrange
        var service = new ScholarshipCalculatorService();
        var career = new Career(
                CareerName.Create("Test"),
                Percentage.Create(51),
                Scholarship.Create(100000)
            );
        career.AddContent(new Content(
            ContentDescription.Create("Test"),
            ContentTypeId.Create("Tecnologico"))
        );
        career.AddArea(new Area(
            AreaDescription.Create("Tecnologia"))
        );

        // Act
        service.Calculate(career);

        // Assert
        career.ScholarshipBudget.Value.Should().Be(340.4);
    }

    [Test]
    public void CalculateScholarship_WhitComputerArea()
    {

        // Arrange
        var service = new ScholarshipCalculatorService();
        var career = new Career(
                CareerName.Create("Test"),
                Percentage.Create(40),
                Scholarship.Create(100000)
            );
        career.AddContent(new Content(
            ContentDescription.Create("Test"),
            ContentTypeId.Create("Tecnologico"))
        );
        career.AddArea(new Area(
            AreaDescription.Create("Tecnologia"))
        );
        career.AddArea(new Area(
            AreaDescription.Create("Computacion e Informatica"))
        );

        // Act
        service.Calculate(career);

        // Assert
        career.ScholarshipBudget.Value.Should().Be(315.5);
    }
}
