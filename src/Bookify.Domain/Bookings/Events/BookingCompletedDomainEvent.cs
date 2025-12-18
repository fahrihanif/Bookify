using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings.Events;

/// <summary>
///     Represents a domain event raised when a booking is completed.
/// </summary>
/// <param name="BookingId">The identifier of the completed booking.</param>
public sealed record BookingCompletedDomainEvent(Guid BookingId) : IDomainEvent;