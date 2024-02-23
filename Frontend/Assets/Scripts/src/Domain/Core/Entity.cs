using System;

namespace TechnicalEvaluation.Domain.Core
{
    /// <summary>
    /// Represents an abstract base class for entities in a domain-driven design.
    /// This class implements IEquatable to provide a way to compare different entities based on their Ids.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier for the entity, which must be a subtype of ValueObject.</typeparam>
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
        where TId : ValueObject // Specifies that TId must be of type ValueObject or its subtype.
    {
        /// <summary>
        /// Gets the identifier of the entity.
        /// </summary>
        public TId Id { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the Entity class with the provided identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        protected Entity(TId id)
        {
            Id = id;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            return obj is Entity<TId> entity && Id.Equals(entity.Id);
        }

        /// <summary>
        /// Determines whether the specified Entity is equal to the current Entity.
        /// </summary>
        /// <param name="other">The Entity to compare with the current Entity.</param>
        /// <returns>true if the specified Entity is equal to the current Entity; otherwise, false.</returns>
        public bool Equals(Entity<TId>? other)
        {
            return Equals((object?)other);
        }

        /// <summary>
        /// Determines whether two instances of Entity are equal.
        /// </summary>
        /// <param name="left">The left-hand side Entity of the comparison.</param>
        /// <param name="right">The right-hand side Entity of the comparison.</param>
        /// <returns>true if left is equal to right; otherwise, false.</returns>
        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether two instances of Entity are not equal.
        /// </summary>
        /// <param name="left">The left-hand side Entity of the comparison.</param>
        /// <param name="right">The right-hand side Entity of the comparison.</param>
        /// <returns>true if left is not equal to right; otherwise, false.</returns>
        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        // Disabling warning CS8618, which relates to non-nullable field uninitialized.
#pragma warning disable CS8618

        /// <summary>
        /// Protected parameterless constructor intended for use by Entity Framework.
        /// </summary>
        protected Entity()
        {
        }

        // Restoring the CS8618 warning state to its original setting after the above declaration.
#pragma warning restore CS8618
    }
}
