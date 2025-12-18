using Bookify.Domain.Abstractions;
using Bookify.Domain.Users.Events;

namespace Bookify.Domain.Users;

/// <summary>
///     Represents a user entity.
/// </summary>
public sealed class User : Entity
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="User" /> class.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="firstName">The first name of the user.</param>
    /// <param name="lastName">The last name of the user.</param>
    /// <param name="email">The email address of the user.</param>
    private User(Guid id, FirstName firstName, LastName lastName, Email email) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    /// <summary>
    ///     Gets the first name of the user.
    /// </summary>
    public FirstName FirstName { get; private set; }

    /// <summary>
    ///     Gets the last name of the user.
    /// </summary>
    public LastName LastName { get; private set; }

    /// <summary>
    ///     Gets the email address of the user.
    /// </summary>
    public Email Email { get; private set; }

    /// <summary>
    ///     Creates a new user with the specified details.
    /// </summary>
    /// <param name="firstName">The first name of the user.</param>
    /// <param name="lastName">The last name of the user.</param>
    /// <param name="email">The email address of the user.</param>
    /// <returns>The created user.</returns>
    public static User Create(FirstName firstName, LastName lastName, Email email)
    {
        var user = new User(Guid.NewGuid(), firstName, lastName, email);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

        return user;
    }
}