using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Domain.Tests.CareerAggregateTests;

public class PercentageTests
{
    [Test]
    public void PercentageTests_Successfully()
    {
        // Arrange
        float value = 0.11111f;

        // Act
        var percentage = Percentage.Create(value);

        // Assert
        percentage.Value.Should().Be(value);
    }

    [Test]
    public void PercentageTests_OutRange()
    {
        // Arrange
        float value = -1f;

        // Act
        Action act = () => Percentage.Create(value);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void CompareEquals_PercentageTests()
    {
        // Arrange
        float value = 10f;

        // Act
        var percentage1 = Percentage.Create(value);
        var percentage2 = Percentage.Create(value);

        // Assert
        percentage1.Should().Be(percentage2);
    }

    [Test]
    public void CompareDiferents_PorcentageTests()
    {
        // Arrange
        float value1 = 10f;
        float value2 = 11f;

        // Act
        var percentage1 = Percentage.Create(value1);
        var percentage2 = Percentage.Create(value2);

        // Assert
        percentage1.Should().NotBe(percentage2);
    }
}
