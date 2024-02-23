using FluentAssertions;
using NUnit.Framework;
using System;
using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Domain.Tests.CareerAggregateTests
{
    public class ContentDescriptionTests
    {
        [Test]
        public void CreatingContentDescription_AssignsValue_Successfully()
        {
            // Arrange
            var description = "Escuela ubicada en Finca 2.";
            // Act
            var contentDescription = ContentDescription.Create(description);

            // Assert
            contentDescription.Value.Should().Be(description,
                because: "the content's description should be assigned through the constructor");
        }

        [Test]
        public void CreatingContentDescription_WithLargeValue_ThrowsException()
        {
            // Arrange
            var description = "La Escuela de Finca 2, fundada en 1960, se ubica estratégicamente cerca de dos edificaciones clave: el Edificio de Ciencias Sociales y el Edificio de Parqueos, este último reservado exclusivamente para el uso de los profesores. A lo largo de su historia, la escuela ha sido administrada por diversos directores";
            // Act
            Action action = () => ContentDescription.Create(description);

            // Assert
            action.Should().Throw<ArgumentException>(
                because: "the content cannot surpass 255 characters");
        }

        [Test]
        public void ComparingContentDescriptions_WithEqualValues_ReturnsEqual()
        {
            // Arrange
            var description = "Escuela de Finca 2.";
        
            // Act
            ContentDescription contentDescription1 = ContentDescription.Create(description);
            ContentDescription contentDescription2 = ContentDescription.Create(description);

            // Assert
            contentDescription1.Should().Be(contentDescription2,
                because: "contents with the same value should be equal");
        }

        [Test]
        public void ComparingContentDescriptions_WithDifferentValues_ReturnsNotEqual()
        {
            // Arrange
            var description1 = "Escuela de Finca 1.";
            var description2 = "Escuela de Finca 2.";

            // Act
            ContentDescription contentDescription1 = ContentDescription.Create(description1);
            ContentDescription contentDescription2 = ContentDescription.Create(description2);

            // Assert
            contentDescription1.Should().NotBe(contentDescription2);
        }

    }
}

