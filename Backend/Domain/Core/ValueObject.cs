namespace TechnicalEvaluation.Domain.Core
{
    /// <summary>
    /// Represents an abstract base class for value objects in a domain-driven design. 
    /// This class provides a way to compare different value objects based on their equality components.
    /// </summary>
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        /// <summary>
        /// Gets the equality components that determine whether two value objects are equal.
        /// Derived classes should override this method to provide the components
        /// that should be compared for equality.
        /// </summary>
        /// <returns>An IEnumerable of objects representing the equality components of the value object.</returns>
        public abstract IEnumerable<object?> GetEqualityComponents();

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType()) return false;

            var valueObject = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        /// <summary>
        /// Determines whether two instances of ValueObject are equal.
        /// </summary>
        /// <param name="left">The left-hand side ValueObject of the comparison.</param>
        /// <param name="right">The right-hand side ValueObject of the comparison.</param>
        /// <returns>true if left is equal to right; otherwise, false.</returns>
        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether two instances of ValueObject are not equal.
        /// </summary>
        /// <param name="left">The left-hand side ValueObject of the comparison.</param>
        /// <param name="right">The right-hand side ValueObject of the comparison.</param>
        /// <returns>true if left is not equal to right; otherwise, false.</returns>
        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Serves as the default hash function, 
        /// which calculates the hash code based on the equality components of the value object.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x?.GetHashCode() ?? 0)
                .Aggregate((x, y) => x ^ y);
        }

        /// <summary>
        /// Determines whether the specified ValueObject is equal to the current ValueObject.
        /// </summary>
        /// <param name="other">The ValueObject to compare with the current ValueObject.</param>
        /// <returns>true if the specified ValueObject is equal to the current ValueObject; otherwise, false.</returns>
        public bool Equals(ValueObject? other)
        {
            return Equals((object?)other);
        }
    }
}
