namespace Bookify.Domain.Abstractions;

/// <summary>
///     Represents an error with a code and message.
/// </summary>
/// <param name="Code">The error code.</param>
/// <param name="Message">The error message.</param>
public record Error(string Code, string Message)
{
    /// <summary>
    ///     Represents a successful operation with no error.
    /// </summary>
    public static Error None = new(string.Empty, string.Empty);

    // <summary>
    /// Represents an error for null values.
    /// </summary>
    public static Error NullValue = new("Error.NullValued", "Value cannot be null.");
}