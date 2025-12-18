using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings.Events;

/// <summary>
///     Represents a domain event raised when a booking is cancelled.
/// </summary>
/// <param name="BookingId">The identifier of the cancelled booking.</param>
public record BookingCancelledDomainEvent(Guid BookingId) : IDomainEvent;