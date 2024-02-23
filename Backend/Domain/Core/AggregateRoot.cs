// Namespace declaration for organization and scope of related classes and types.
namespace TechnicalEvaluation.Domain.Core
{
    /// <summary>
    /// Represents a base abstract class for an Aggregate Root in the context of Domain-Driven Design.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier for the aggregate root, which must be a subtype of ValueObject.</typeparam>
    public abstract class AggregateRoot<TId> : Entity<TId>
        where TId : ValueObject // Constrain that TId must be of type ValueObject or its subtype.
    {
        /// <summary>
        /// Parameterized constructor that initializes the AggregateRoot with the provided identifier.
        /// </summary>
        /// <param name="id">The identifier of the AggregateRoot of type TId.</param>
        protected AggregateRoot(TId id) : base(id)
        {
        }

        // Disabling warning CS8618, which relates to non-nullable field uninitialized.
#pragma warning disable CS8618

        /// <summary>
        /// Protected parameterless constructor intended for use by Entity Framework.
        /// </summary>
        protected AggregateRoot()
        {
        }

        // Restoring the CS8618 warning state to its original setting after the above declaration.
#pragma warning restore CS8618
    }
}
