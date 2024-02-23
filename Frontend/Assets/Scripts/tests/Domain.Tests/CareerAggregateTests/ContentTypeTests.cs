using FluentAssertions;
using NUnit.Framework;
using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Domain.Tests.CareerAggregateTests
{
    public class ContentTypeTests
    {
        [Test]
        public void CreatingContentType_Successfully()
        {
            // Arrange
            var id = "Test";
            var contentTypeId = ContentTypeId.Create(id);

            // Act
            var contentType = new ContentType(contentTypeId);

            // Assert
            contentType.Id.Should().Be(contentTypeId);
        }

        [Test]
        public void CompareEqual_ContentTypes()
        {
            // Arrange
            var id = "Test";
            var contentTypeId = ContentTypeId.Create(id);

            // Act
            var contentType1 = new ContentType(contentTypeId);
            var contentType2 = new ContentType(contentTypeId);

            // Assert
            contentType1.Should().Be(contentType2);
        }

        [Test]
        public void CompareDifferents_ContentTypeIds()
        {
            // Arrange
            var contentTypeId1 = ContentTypeId.Create("Test1");
            var contentTypeId2 = ContentTypeId.Create("Test2");

            // Act
            var contentType1 = new ContentType(contentTypeId1);
            var contentType2 = new ContentType(contentTypeId2);

            // Assert
            contentType1.Should().NotBe(contentType2);
        }
    }
}

