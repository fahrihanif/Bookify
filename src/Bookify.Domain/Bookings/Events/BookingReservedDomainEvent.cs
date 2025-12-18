using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings.Events;

/// <summary>
///     Represents a domain event raised when a booking is reserved.
/// </summary>
/// <param name="BookingId">The identifier of the reserved booking.</param>
public sealed record BookingReservedDomainEvent(Guid BookingId) : IDomainEvent;