using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings;

/// <summary>
///     Contains predefined error instances for booking operations.
/// </summary>
public static class BookingErrors
{
    /// <summary>
    ///     Error indicating that the booking with the given ID was not found.
    /// </summary>
    public static Error NotFound = new("Booking.NotFound", "The booking with the given id was not found.");

    /// <summary>
    ///     Error indicating that the booking overlaps with an existing booking.
    /// </summary>
    public static Error Overlap = new("Booking.Overlap", "The booking overlaps with an existing booking.");

    /// <summary>
    ///     Error indicating that the booking is not in the reserved status.
    /// </summary>
    public static Error NotReserved = new("Booking.NotReserved", "The booking is not reserved.");

    /// <summary>
    ///     Error indicating that the booking is not in the confirmed status.
    /// </summary>
    public static Error NotConfirmed = new("Booking.NotConfirmed", "The booking is not confirmed.");

    /// <summary>
    ///     Error indicating that the booking has already started.
    /// </summary>
    public static Error AlreadyStarted = new("Booking.AlreadyStarted", "The booking has already started.");
}