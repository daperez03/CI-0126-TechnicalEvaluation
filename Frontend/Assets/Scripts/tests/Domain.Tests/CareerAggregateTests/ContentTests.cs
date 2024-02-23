using FluentAssertions;
using NUnit.Framework;
using System;
using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Domain.Tests.CareerAggregateTests
{
    public class ContentTests
    {
        [Test]
        public void CreatingContent_AssignsContentDescription_Successfully()
        {
            // Arrange
            var contentDescription = ContentDescription.Create("Escuela ubicada en Finca 2.");
            var contentType = ContentTypeId.Create("Test");

            // Act
            var content = new Content(contentDescription, contentType);

            // Assert
            content.Id.Should().Be(contentDescription,
                because: "the content's description should be assigned through the constructor");
        }

        [Test]
        public void ComparingContents_WithSameId_ReturnsEqual()
        {
            // Arrange
            var contentDescription = ContentDescription.Create("Escuela ubicada en Finca 2.");
            var contentType = ContentTypeId.Create("Test");

            // Act
            var content1 = new Content(contentDescription, contentType);
            var content2 = new Content(contentDescription, contentType);

            // Assert
            content1.Should().Be(content2,
                because: "contents with the same ID should be equal");
        }

        [Test]
        public void ComparingContents_WithDifferentIds_ReturnsEqual()
        {
            // Arrange
            var contentDescription1 = ContentDescription.Create("Escuela ubicada en Finca 2.");
            var contentDescription2 = ContentDescription.Create("Escuela ubicada en Finca 1.");
            var contentType = ContentTypeId.Create("Test");

            // Act
            var content1 = new Content(contentDescription1, contentType);
            var content2 = new Content(contentDescription2, contentType);

            // Assert
            content1.Should().NotBe(content2,
                because: "contents with the same ID should be equal");
        }

        [Test]
        public void ComparingContents_WithSameType_ReturnsEqual()
        {
            // Arrange
            var contentDescription = ContentDescription.Create("Escuela ubicada en Finca 2.");
            var contentType = ContentTypeId.Create("Test");

            // Act
            var content1 = new Content(contentDescription, contentType);
            var content2 = new Content(contentDescription, contentType);

            // Assert
            content1.ContentType.Should().Be(content2.ContentType);
        }

        [Test]
        public void ComparingContents_WithDifferentType_ReturnsEqual()
        {
            // Arrange
            var contentDescription1 = ContentDescription.Create("Escuela ubicada en Finca 2.");
            var contentDescription2 = ContentDescription.Create("Escuela ubicada en Finca 1.");
            var contentType1 = ContentTypeId.Create("Test1");
            var contentType2 = ContentTypeId.Create("Test2");

            // Act
            var content1 = new Content(contentDescription1, contentType1);
            var content2 = new Content(contentDescription2, contentType2);

            // Assert
            content1.Should().NotBe(content2,
                because: "contents with the same ID should be equal");
        }

        [Test]
        public void AssigningCareerToContent_ThatHasNotBeenAssigned_ExecutesSuccessfully()
        {
            // Arrange
            var career = new Career(
                CareerName.Create("Filosofia"),
                Percentage.Create(10),
                Scholarship.Create(100)
            );
            var contentDescription = ContentDescription.Create("Creada en 1850");
            var contentType = ContentTypeId.Create("Test");
            var content = new Content(contentDescription, contentType);

            // Act
            Action act = () => content.AssignCareer(career);

            // Assert
            act.Should().NotThrow<InvalidOperationException>(
                because: "adding unassigned career to content should not throw an exception");
        }

        [Test]
        public void AssigningCareerToContent_ThatAlreadyHasACareer_ThrowsException()
        {
            // Arrange
            var career = new Career(
                CareerName.Create("Filosofia"),
                Percentage.Create(10),
                Scholarship.Create(100)
            );
            var contentDescription = ContentDescription.Create("Creada en 1850");
            var contentType = ContentTypeId.Create("Test");
            var content = new Content(contentDescription, contentType);
            content.AssignCareer(career);

            // Act
            Action act = () => content.AssignCareer(career);

            // Assert
            act.Should().Throw<InvalidOperationException>(
                because: "adding a career to a content that already has a career assigned should throw an exception");
        }

        [Test]
        public void UnssigningCareerToContent_ThatAlreadyHasACareer_ExecutesSucessfully()
        {
            // Arrange
            var career = new Career(
                CareerName.Create("Filosofia"),
                Percentage.Create(10),
                Scholarship.Create(100)
            );
            var contentDescription = ContentDescription.Create("Creada en 1850");
            var contentType = ContentTypeId.Create("Test");
            var content = new Content(contentDescription, contentType);
            content.AssignCareer(career);

            // Act
            content.UnassignCareer();

            // Assert
            content.Career.Should().BeNull(
                because: "unassigning a career should set it to null");
        }

    }
}

