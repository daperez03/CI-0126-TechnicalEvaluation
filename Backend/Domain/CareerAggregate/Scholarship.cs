using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalEvaluation.Domain.Core;

namespace TechnicalEvaluation.Domain.CareerAggregate;

/// <summary>
/// Represents a scholarship value in the application.
/// </summary>
public class Scholarship : ValueObject
{
    /// <summary>
    /// Gets the double value of the scholarship.
    /// </summary>
    public double Value { get; }

    /// <summary>
    /// Private constructor for creating a new instance of <see cref="Scholarship"/>.
    /// </summary>
    /// <param name="value">The double value representing the scholarship.</param>
    private Scholarship(double value)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a new instance of <see cref="Scholarship"/> with the specified value.
    /// </summary>
    /// <param name="value">The double value representing the scholarship.</param>
    /// <returns>A new instance of <see cref="Scholarship"/>.</returns>
    public static Scholarship Create(double value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Scholarships cannot be less than zero.", nameof(value));
        }

        return new Scholarship(value);
    }

    /// <summary>
    /// Retrieves the equality components for comparison.
    /// </summary>
    /// <returns>An IEnumerable of the equality components.</returns>
    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
