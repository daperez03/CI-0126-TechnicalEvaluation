using System;
using System.Collections.Generic;
using TechnicalEvaluation.Domain.Core;

namespace TechnicalEvaluation.Domain.CareerAggregate
{
    /// <summary>
    /// Represents the ContentDescription value object within the CareerAggregate, 
    /// adhering to the value object semantics in domain-driven design.
    /// </summary>
    public class ContentDescription : ValueObject
    {
        /// <summary>
        /// Gets the value of the ContentDescription.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Initializes a new instance of the ContentDescription class with the provided value.
        /// </summary>
        /// <param name="value">The value of the ContentDescription.</param>
        private ContentDescription(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Creates a new instance of ContentDescription, ensuring the provided value adheres to the established invariants.
        /// </summary>
        /// <param name="value">The string value used to initialize the ContentDescription object.</param>
        /// <returns>A new instance of ContentDescription initialized with the provided value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the provided value surpasses 255 characters in length.
        /// </exception>
        public static ContentDescription Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Content description cannot be null.", nameof(value));
            }

            if (value.Length > 255)
            {
                throw new ArgumentException("Content description cannot surpass 255 characters.", nameof(value));
            }

            return new ContentDescription(value);
        }

        /// <summary>
        /// Provides the sequence of components that affects the equality of the ContentDescription object.
        /// </summary>
        /// <returns>A sequence containing the Value of the ContentDescription.</returns>
        public override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
