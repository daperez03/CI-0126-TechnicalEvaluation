using System;
using TechnicalEvaluation.Domain.Core;

namespace TechnicalEvaluation.Domain.CareerAggregate
{
    /// <summary>
    /// Represents a Content entity in the CareerAggregate. 
    /// </summary>
    public class Content : Entity<ContentDescription>
    {
        /// <summary>
        /// Gets the Career object to which this Content is assigned, if any.
        /// </summary>
        public Career? Career { get; private set; }

        /// <summary>
        /// Gets or sets the content type associated with the object.
        /// </summary>
        public ContentTypeId ContentType { get; private set; }


        /// <summary>
        /// Initializes a new instance of the Content class with the provided description.
        /// </summary>
        /// <param name="description">The description of the Content entity, of type ContentDescription.</param>
        public Content(ContentDescription description, ContentTypeId contentType)
            : base(description)
        {
            ContentType = contentType;
        }

        /// <summary>
        /// Private parameterless constructor intended for use by Entity Framework Core.
        /// Not intended for general use.
        /// </summary>
        private Content()
        {
        }

        /// <summary>
        /// Assigns a Career to the Content entity while enforcing business rules.
        /// </summary>
        /// <param name="career">The Career entity to assign to this Content.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when trying to assign a Career to a Content entity that already belongs to a Career.
        /// </exception>
        public void AssignCareer(Career career)
        {
            if (Career is not null)
            {
                throw new InvalidOperationException("Cannot assign career to a content that already belongs to a career.");
            }
            Career = career;
        }

        /// <summary>
        /// Unassigns any Career currently assigned to the Content entity.
        /// </summary>
        public void UnassignCareer()
        {
            Career = null;
        }
    }
}
