using FluentAssertions;
using NUnit.Framework;
using System;
using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Domain.Tests.CareerAggregateTests
{
    public class CareerNameTests
    {
        [Test]
        public void CreatingCareerName_AssignsValue_Successfully()
        {
            // Arrage
            var value = "CareerName";

            // Act
            var careerName = CareerName.Create(value);

            // Asseert
            careerName.Value.Should().Be(value,
                because: "Verifies that a career name is successfully assigned.");
        }

        [Test]
        public void CreatingCareerName_ThrowsException_WithLongName()
        {
            // Arrage
            var value = "CareerNameWithLongerThanThirtyOne";
            Exception? exception = null;

            // Act
            try
            {
                var careerName = CareerName.Create(value);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            // Asseert
            exception.Should().NotBeNull(
                because: "Career name cannot have more than 30 characters");
        }

        [Test]
        public void CreatingCareerName_ThrowsException_WithNullName()
        {
            // Arrage
            string? value = null;
            Exception? exception = null;

            // Act
            try
            {
                var careerName = CareerName.Create(value);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            // Asseert
            exception.Should().NotBeNull(
                because: "Career Name cannot be null");
        }

        [Test]
        public void ComparingEqualCareerName_ReturnResult_Successfully()
        {
            // Arrage
            var value = "CareerName";

            // Act
            var careerName1 = CareerName.Create(value);
            var careerName2 = CareerName.Create(value);

            // Asseert
            careerName1.Should().Be(careerName2,
                because: "Ensures that comparing two equal career names results in success.");
        }

        [Test]
        public void ComparingDifferentCareerName_ReturnResult_Successfully()
        {
            // Arrage
            var value1 = "CareerName1";
            var value2 = "CareerName2";

            // Act
            var careerName1 = CareerName.Create(value1);
            var careerName2 = CareerName.Create(value2);

            // Asseert
            careerName1.Should().NotBe(careerName2,
                because: "Ensures that comparing two different career names results in success.");
        }
    }
}

