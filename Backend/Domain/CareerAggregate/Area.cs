using System;
using TechnicalEvaluation.Domain.Core;

namespace TechnicalEvaluation.Domain.CareerAggregate
{
    /// <summary>
    /// Represents an Area entity in the CareerAggregate. 
    /// </summary>
    public class Area : Entity<AreaDescription>
    {
        /// <summary>
        /// Gets a read-only collection of the Career entities associated with the Area.
        /// </summary>
        public IReadOnlyCollection<Career> Careers => _careers.AsReadOnly();

        /// <summary>
        /// Represents a private list to hold Career entities.
        /// </summary>
        private readonly List<Career> _careers = new();


        /// <summary>
        /// Initializes a new instance of the Area class with the provided description.
        /// </summary>
        /// <param name="description">The description of the Area entity, of type AreaDescription.</param>
        public Area(AreaDescription description)
            : base(description)
        {
        }

        /// <summary>
        /// Private parameterless constructor intended for use by Entity Framework Core.
        /// Not intended for general use.
        /// </summary>
        private Area()
        {
        }

        public void AssignCareer(Career career)
        {
            if (career is null)
            {
                throw new ArgumentNullException("Cannot assign a null career.");
            }
            if (_careers.Exists(c => c == career))
            {
                throw new InvalidOperationException("Area already belongs to the career.");
            }
            _careers.Add(career);
        }


        public void UnassignCareer(CareerName name)
        {
            if (name is null)
            {
                throw new ArgumentNullException("Cannot unassign a null career.");
            }
            var nameToRemove = _careers.FirstOrDefault(c => c.Id == name);
            if (nameToRemove is null)
            {
                throw new InvalidOperationException("Area doesn't belong to the career.");
            }
            _careers.Remove(nameToRemove);
        }
    }
}
