using System;
using System.Collections.Generic;
using TechnicalEvaluation.Domain.Core;

namespace TechnicalEvaluation.Domain.CareerAggregate
{
    /// <summary>
    /// Represents the CareerName value object within the CareerAggregate, 
    /// adhering to the value object semantics in domain-driven design.
    /// </summary>
    public class CareerName : ValueObject
    {
        /// <summary>
        /// Gets the value of the CareerName.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Initializes a new instance of the CareerName class with the provided value.
        /// </summary>
        /// <param name="value">The value of the CareerName.</param>
        private CareerName(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Creates a new instance of CareerName, ensuring the provided value adheres to the established invariants.
        /// </summary>
        /// <param name="value">The string value used to initialize the CareerName object.</param>
        /// <returns>A new instance of CareerName initialized with the provided value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the provided value surpasses 30 characters in length.
        /// </exception>
        public static CareerName Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Career name cannot be null.", nameof(value));
            }
            
            if (value.Length > 30)
            {
                throw new ArgumentException("Career name cannot surpass 30 characters.", nameof(value));
            }

            return new CareerName(value);
        }

        /// <summary>
        /// Provides the sequence of components that affects the equality of the CareerName object.
        /// </summary>
        /// <returns>A sequence containing the Value of the CareerName.</returns>
        public override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        /// <summary>
        /// Default conversion to string
        /// </summary>
        /// <returns>String representation of career name.</returns>
        public override string ToString() => Value;

        /// <summary>
        /// Explicit conversion to string type, by calling ToString method.
        /// </summary>
        /// <param name="value">CareerName object to convert to string.</param>
        public static explicit operator string(CareerName value) => value.ToString();
    }
}
