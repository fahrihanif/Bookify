using System.Diagnostics.CodeAnalysis;

namespace Bookify.Domain.Abstractions;

/// <summary>
///     Represents the result of an operation, indicating success or failure.
/// </summary>
public class Result
{
    // <summary>
    /// Initializes a new instance of the
    /// <see cref="Result" />
    /// class.
    /// </summary>
    /// <param name="isSuccess">True if the operation was successful; otherwise, false.</param>
    /// <param name="error">The error associated with the failure, or <see cref="Error.None" /> for success.</param>
    /// <exception cref="InvalidOperationException">Thrown if the parameters are inconsistent.</exception>
    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
            throw new InvalidOperationException();

        if (!isSuccess && error == Error.None)
            throw new InvalidOperationException();

        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>
    ///     Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    ///     Gets a value indicating whether the operation failed.
    /// </summary>
    public bool IsFailure
        => !IsSuccess;

    /// <summary>
    ///     Gets the error associated with the operation, if it failed.
    /// </summary>
    public Error Error { get; }

    /// <summary>
    ///     Creates a successful result.
    /// </summary>
    /// <returns>A <see cref="Result" /> indicating success.</returns>
    public static Result Success()
        => new(true, Error.None);

    /// <summary>
    ///     Creates a failed result with the specified error.
    /// </summary>
    /// <param name="error">The error describing the failure.</param>
    /// <returns>A <see cref="Result" /> indicating failure.</returns>
    public static Result Failure(Error error)
        => new(false, error);

    /// <summary>
    ///     Creates a successful result with a value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="value">The value to include in the result.</param>
    /// <returns>A <see cref="Result{TValue}" /> indicating success.</returns>
    public static Result<TValue> Success<TValue>(TValue value)
        => new(value, true, Error.None);

    /// <summary>
    ///     Creates a failed result with the specified error.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="error">The error describing the failure.</param>
    /// <returns>A <see cref="Result{TValue}" /> indicating failure.</returns>
    public static Result<TValue> Failure<TValue>(Error error)
        => new(default, false, error);

    /// <summary>
    ///     Creates a result based on whether the value is null.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>A <see cref="Result{TValue}" /> with the value if not null, or a failure result.</returns>
    public static Result<TValue> Create<TValue>(TValue? value)
        => value is null
            ? Failure<TValue>(Error.NullValue)
            : Success(value);
}

/// <summary>
///     Represents the result of an operation with a value, indicating success or failure.
/// </summary>
/// <typeparam name="TValue">The type of the value.</typeparam>
public class Result<TValue> : Result
{
    private readonly TValue? _value;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Result{TValue}" /> class.
    /// </summary>
    /// <param name="value">The value associated with the result.</param>
    /// <param name="isSuccess">True if the operation was successful; otherwise, false.</param>
    /// <param name="error">The error associated with the failure, or <see cref="Error.None" /> for success.</param>
    protected internal Result(TValue? value, bool isSuccess, Error error) : base(isSuccess, error)
        => _value = value;

    /// <summary>
    ///     Gets the value associated with the result.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the result indicates failure.</exception>
    [NotNull]
    public TValue Value
        => IsSuccess
            ? _value!
            : throw new InvalidOperationException("Cannot get value of failed result.");

    /// <summary>
    ///     Implicitly converts a value to a <see cref="Result{TValue}" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static implicit operator Result<TValue>(TValue? value)
        => Create(value);
}