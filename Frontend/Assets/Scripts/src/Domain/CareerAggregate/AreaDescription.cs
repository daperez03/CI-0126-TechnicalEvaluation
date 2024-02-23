using System;
using System.Collections.Generic;
using TechnicalEvaluation.Domain.Core;

namespace TechnicalEvaluation.Domain.CareerAggregate
{
    /// <summary>
    /// Represents the AreaDescription value object within the CareerAggregate, 
    /// adhering to the value object semantics in domain-driven design.
    /// </summary>
    public class AreaDescription : ValueObject
    {
        /// <summary>
        /// Gets the value of the AreaDescription.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Initializes a new instance of the AreaDescription class with the provided value.
        /// </summary>
        /// <param name="value">The value of the AreaDescription.</param>
        private AreaDescription(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Creates a new instance of AreaDescription, ensuring the provided value adheres to the established invariants.
        /// </summary>
        /// <param name="value">The string value used to initialize the AreaDescription object.</param>
        /// <returns>A new instance of AreaDescription initialized with the provided value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the provided value surpasses 30 characters in length.
        /// </exception>
        public static AreaDescription Create(string value)
        {
            if (value.Length > 30)
            {
                throw new ArgumentException("Area description cannot surpass 30 characters.", nameof(value));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("Area description cannot be null.", nameof(value));
            }

            return new AreaDescription(value);
        }

        /// <summary>
        /// Provides the sequence of components that affects the equality of the AreaDescription object.
        /// </summary>
        /// <returns>A sequence containing the Value of the AreaDescription.</returns>
        public override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
