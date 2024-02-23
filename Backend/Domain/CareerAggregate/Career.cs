using System.Collections.Generic;
using System.Linq;
using System;
using TechnicalEvaluation.Domain.Core;

namespace TechnicalEvaluation.Domain.CareerAggregate
{
    /// <summary>
    /// Represents a Career entity in the domain model, extending from the generic AggregateRoot class with CareerName as the identifier.
    /// </summary>
    public class Career : AggregateRoot<CareerName>
    {
        /// <summary>
        /// Gets a read-only collection of the Content entities associated with the Career.
        /// </summary>
        public IReadOnlyCollection<Content> Contents => _contents.AsReadOnly();

        /// <summary>
        /// Represents a private list to hold Content entities.
        /// </summary>
        private readonly List<Content> _contents = new();



        /// <summary>
        /// Gets a read-only collection of the Area entities associated with the Career.
        /// </summary>
        public IReadOnlyCollection<Area> Areas => _areas.AsReadOnly();

        /// <summary>
        /// Represents a private list to hold Area entities.
        /// </summary>
        private readonly List<Area> _areas = new();

        /// <summary>
        /// Gets or sets the percentage of women associated with the object.
        /// </summary>
        public Percentage WomenPercentage { get; set; }

        /// <summary>
        /// Gets or sets the scholarship budget associated with the object.
        /// </summary>
        public Scholarship ScholarshipBudget { get; set; }

        /// <summary>
        /// Initializes a new instance of the Career class with the provided name.
        /// </summary>
        /// <param name="name">The name of the Career.</param>
        public Career(
            CareerName name, 
            Percentage womenPercentage, 
            Scholarship scholarshipBudget)
            : base(name)
        {
            WomenPercentage = womenPercentage;
            ScholarshipBudget = scholarshipBudget;
        }

        /// <summary>
        /// Private parameterless constructor for Entity Framework Core. Not intended for general use.
        /// </summary>
        [Obsolete("Do not use this constructor, it is only meant for EFCore / Moq.")]
        protected Career()
        {
        }

        /// <summary>
        /// Adds a Content entity to the Career if it does not already exist.
        /// </summary>
        /// <param name="content">The Content entity to add.</param>
        /// <exception cref="InvalidOperationException">Thrown when the content already belongs to the career.</exception>
        public void AddContent(Content content)
        {
            if (_contents.Exists(c => c == content))
            {
                throw new InvalidOperationException("Content already belongs to the career.");
            }
            _contents.Add(content);
            content.AssignCareer(this);
        }

        /// <summary>
        /// Removes a Content entity from the Career based on its description.
        /// </summary>
        /// <param name="description">The description of the Content entity to remove.</param>
        /// <exception cref="InvalidOperationException">Thrown when the content doesn't belong to the career.</exception>
        public void RemoveContent(ContentDescription description)
        {
            var descriptionToRemove = _contents.FirstOrDefault(c => c.Id == description);
            if (descriptionToRemove is null)
            {
                throw new InvalidOperationException("Content doesn't belong to career.");
            }
            _contents.Remove(descriptionToRemove);
            descriptionToRemove.UnassignCareer();
        }



        /// <summary>
        /// Adds a Content entity to the Career if it does not already exist.
        /// </summary>
        /// <param name="content">The Content entity to add.</param>
        /// <exception cref="InvalidOperationException">Thrown when the content already belongs to the career.</exception>
        public void AddArea(Area area)
        {
            if (_areas.Exists(a => a == area))
            {
                throw new InvalidOperationException("Area already belongs to the career.");
            }
            _areas.Add(area);
        }

        /// <summary>
        /// Removes a Content entity from the Career based on its description.
        /// </summary>
        /// <param name="description">The description of the Content entity to remove.</param>
        /// <exception cref="InvalidOperationException">Thrown when the content doesn't belong to the career.</exception>
        public void RemoveArea(AreaDescription description)
        {
            var areaToRemove = _areas.FirstOrDefault(a => a.Id == description);
            if (areaToRemove is null)
            {
                throw new InvalidOperationException("Area doesn't belong to the career.");
            }
            _areas.Remove(areaToRemove);
            areaToRemove.UnassignCareer(this.Id);
        }


    }
}
