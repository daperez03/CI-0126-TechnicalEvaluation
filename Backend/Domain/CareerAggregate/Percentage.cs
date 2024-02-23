

using TechnicalEvaluation.Domain.Core;

namespace TechnicalEvaluation.Domain.CareerAggregate;

/// <summary>
/// Represents a percentage value in the application.
/// </summary>
public class Percentage : ValueObject
{
    /// <summary>
    /// Gets the float value of the percentage.
    /// </summary>
    public float Value { get; }

    /// <summary>
    /// Private constructor for creating a new instance of <see cref="Percentage"/>.
    /// </summary>
    /// <param name="value">The float value representing the percentage.</param>
    private Percentage(float value)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a new instance of <see cref="Percentage"/> with the specified value.
    /// </summary>
    /// <param name="value">The float value representing the percentage.</param>
    /// <returns>A new instance of <see cref="Percentage"/>.</returns>
    public static Percentage Create(float value)
    {
        if (value > 100 || value < 0)
        {
            throw new ArgumentException("Percentage is out of range (0-100).", nameof(value));
        }

        return new Percentage(value);
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
