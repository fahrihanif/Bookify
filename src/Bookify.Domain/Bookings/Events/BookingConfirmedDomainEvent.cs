using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings.Events;

/// <summary>
///     Represents a domain event raised when a booking is confirmed.
/// </summary>
/// <param name="BookingId">The identifier of the confirmed booking.</param>
public sealed record BookingConfirmedDomainEvent(Guid BookingId) : IDomainEvent;