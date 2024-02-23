using System;
using TechnicalEvaluation.Domain.Core;

namespace TechnicalEvaluation.Domain.CareerAggregate
{
    /// <summary>
    /// Represents a content type in the application domain.
    /// </summary>
    public class ContentType : Entity<ContentTypeId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentType"/> class with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier associated with the content type.</param>
        public ContentType(ContentTypeId id)
            : base(id)
        {
        }

        /// <summary>
        /// Obsolete constructor for EFCore. Do not use directly in application logic.
        /// </summary>
        [Obsolete("For EFCore")]
        public ContentType()
        {
        }
    }
}

