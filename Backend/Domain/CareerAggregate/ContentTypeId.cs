using TechnicalEvaluation.Domain.Core;

namespace TechnicalEvaluation.Domain.CareerAggregate;

/// <summary>
/// Represents the unique identifier for a content type in the application.
/// </summary>
public class ContentTypeId : ValueObject
{
    /// <summary>
    /// Gets the string value of the content type identifier.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Private constructor for creating a new instance of <see cref="ContentTypeId"/>.
    /// </summary>
    /// <param name="value">The string value representing the content type identifier.</param>
    private ContentTypeId(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a new instance of <see cref="ContentTypeId"/> with the specified value.
    /// </summary>
    /// <param name="value">The string value representing the content type identifier.</param>
    /// <returns>A new instance of <see cref="ContentTypeId"/>.</returns>
    public static ContentTypeId Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("ContentTypeId cannot be null or empty.", nameof(value));
        }

        if (value.Length > 30)
        {
            throw new ArgumentException("ContentTypeId cannot surpass 30 characters.", nameof(value));
        }

        return new ContentTypeId(value);
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

