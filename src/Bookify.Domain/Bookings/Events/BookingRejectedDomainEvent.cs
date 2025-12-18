using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings.Events;

/// <summary>
///     Represents a domain event raised when a booking is rejected.
/// </summary>
/// <param name="BookingId">The identifier of the rejected booking.</param>
public sealed record BookingRejectedDomainEvent(Guid BookingId) : IDomainEvent;