using FluentAssertions;
using NUnit.Framework;
using System;
using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Domain.Tests.CareerAggregateTests
{
    public class ScholarshipTests
    {
        [Test]
        public void CreateScholarship_Successfully()
        {
            // Arrange
            double value = 0;

            // Act
            var scholarship = Scholarship.Create(value);

            // Assert
            scholarship.Value.Should().Be(value);
        }

        [Test]
        public void CreateScholarship_LessThatZero()
        {
            // Arrange
            double value = -1;

            // Act
            Action act = () => Scholarship.Create(value);

            // Assert
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void CompareEquals_PercentageTests()
        {
            // Arrange
            double value = 10;

            // Act
            var scholarship1 = Scholarship.Create(value);
            var scholarship2 = Scholarship.Create(value);

            // Assert
            scholarship1.Should().Be(scholarship2);
        }

        [Test]
        public void CompareDiferents_PorcentageTests()
        {
            // Arrange
            double value1 = 10;
            double value2 = 11;

            // Act
            var scholarship1 = Scholarship.Create(value1);
            var scholarship2 = Scholarship.Create(value2);

            // Assert
            scholarship1.Should().NotBe(scholarship2);
        }
    }
}

