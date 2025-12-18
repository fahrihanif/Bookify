using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Users.Events;

/// <summary>
///     Represents a domain event raised when a user is created.
/// </summary>
/// <param name="UserId">The identifier of the created user.</param>
public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent { }