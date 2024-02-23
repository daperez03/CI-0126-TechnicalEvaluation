using FluentAssertions;
using NUnit.Framework;
using System;
using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Domain.Tests.CareerAggregateTests
{
    public class ContentTypeIdTests
    {
        [Test]
        public void CreatingContentTypeId_Successfully()
        {
            // Arrange
            var id = "Test";
        
            // Act
            var contentTypeId = ContentTypeId.Create(id);

            // Assert
            contentTypeId.Value.Should().Be(id);
        }

        [Test]
        public void CreatingContentTypeId_WithNullId_ThrowException()
        {
            // Arrange
            string id = null;

            // Act
            Action contentTypeId = () => ContentTypeId.Create(id);

            // Assert
            contentTypeId.Should().Throw<ArgumentException>();
        }

        [Test]
        public void CreatingContentTypeId_With31Chars_ThrowException()
        {
            // Arrange
            string id = "1234567890123456789012345678901";

            // Act
            Action contentTypeId = () => ContentTypeId.Create(id);

            // Assert
            contentTypeId.Should().Throw<ArgumentException>();
        }

        [Test]
        public void CompareEquals_ContentTypeIds()
        {
            // Arrange
            var id = "Test";

            // Act
            var contentTypeId1 = ContentTypeId.Create(id);
            var contentTypeId2 = ContentTypeId.Create(id);

            // Assert
            contentTypeId1.Should().Be(contentTypeId2);
        }

        [Test]
        public void CompareDifferents_ContentTypeIds()
        {
            // Arrange
            var id1 = "Test1";
            var id2 = "Test2";

            // Act
            var contentTypeId1 = ContentTypeId.Create(id1);
            var contentTypeId2 = ContentTypeId.Create(id2);

            // Assert
            contentTypeId1.Should().NotBe(contentTypeId2);
        }
    }
}

