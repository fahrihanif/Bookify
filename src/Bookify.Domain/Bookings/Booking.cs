using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings.Events;

namespace Bookify.Domain.Bookings;

/// <summary>
///     Represents a booking entity.
/// </summary>
public sealed class Booking : Entity
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Booking" /> class.
    /// </summary>
    /// <param name="id">The unique identifier of the booking.</param>
    /// <param name="apartmentId">The identifier of the apartment being booked.</param>
    /// <param name="userId">The identifier of the user making the booking.</param>
    /// <param name="duration">The duration of the booking.</param>
    /// <param name="priceForPeriod">The price for the booking period.</param>
    /// <param name="cleaningFee">The cleaning fee.</param>
    /// <param name="amenitiesUpCharge">The amenities upcharge.</param>
    /// <param name="totalPrice">The total price of the booking.</param>
    /// <param name="status">The status of the booking.</param>
    /// <param name="createdOnUtc">The date and time when the booking was created, in UTC.</param>
    private Booking(
        Guid id,
        Guid apartmentId,
        Guid userId,
        DateRange duration,
        Money priceForPeriod,
        Money cleaningFee,
        Money amenitiesUpCharge,
        Money totalPrice,
        BookingStatus status,
        DateTime createdOnUtc) : base(id)
    {
        ApartmentId = apartmentId;
        UserId = userId;
        Duration = duration;
        PriceForPeriod = priceForPeriod;
        CleaningFee = cleaningFee;
        AmenitiesUpCharge = amenitiesUpCharge;
        TotalPrice = totalPrice;
        Status = status;
        CreatedOnUtc = createdOnUtc;
    }

    /// <summary>
    ///     Gets the identifier of the apartment being booked.
    /// </summary>
    public Guid ApartmentId { get; private set; }

    /// <summary>
    ///     Gets the identifier of the user making the booking.
    /// </summary>
    public Guid UserId { get; private set; }

    /// <summary>
    ///     Gets the duration of the booking.
    /// </summary>
    public DateRange Duration { get; }

    /// <summary>
    ///     Gets the price for the booking period.
    /// </summary>
    public Money PriceForPeriod { get; private set; }

    /// <summary>
    ///     Gets the cleaning fee.
    /// </summary>
    public Money CleaningFee { get; private set; }

    /// <summary>
    ///     Gets the amenities upcharge.
    /// </summary>
    public Money AmenitiesUpCharge { get; private set; }

    /// <summary>
    ///     Gets the total price of the booking.
    /// </summary>
    public Money TotalPrice { get; private set; }

    /// <summary>
    ///     Gets the status of the booking.
    /// </summary>
    public BookingStatus Status { get; private set; }

    /// <summary>
    ///     Gets the date and time when the booking was created, in UTC.
    /// </summary>
    public DateTime CreatedOnUtc { get; private set; }

    /// <summary>
    ///     Gets the date and time when the booking was confirmed, in UTC.
    /// </summary>
    public DateTime? ConfirmedOnUtc { get; private set; }

    /// <summary>
    ///     Gets the date and time when the booking was rejected, in UTC.
    /// </summary>
    public DateTime? RejectedOnUtc { get; private set; }

    /// <summary>
    ///     Gets the date and time when the booking was completed, in UTC.
    /// </summary>
    public DateTime? CompletedOnUtc { get; private set; }

    /// <summary>
    ///     Gets the date and time when the booking was cancelled, in UTC.
    /// </summary>
    public DateTime? CancelledOnUtc { get; private set; }

    /// <summary>
    ///     Reserves a new booking for the specified apartment and user.
    /// </summary>
    /// <param name="apartment">The apartment to book.</param>
    /// <param name="userId">The identifier of the user making the booking.</param>
    /// <param name="duration">The duration of the booking.</param>
    /// <param name="utcNow">The current date and time in UTC.</param>
    /// <param name="pricingService">The pricing service to calculate the price.</param>
    /// <returns>The reserved booking.</returns>
    public static Booking Reserve(
        Apartment apartment,
        Guid userId,
        DateRange duration,
        DateTime utcNow,
        PricingService pricingService)
    {
        var pricingDetails = pricingService.CalculatePrice(apartment, duration);

        var booking = new Booking(Guid.NewGuid(),
                                  apartment.Id,
                                  userId,
                                  duration,
                                  pricingDetails.PriceForPeriod,
                                  pricingDetails.CleaningFee,
                                  pricingDetails.AmenitiesUpCharge,
                                  pricingDetails.TotalPrice,
                                  BookingStatus.Reserved,
                                  utcNow);

        booking.RaiseDomainEvent(new BookingReservedDomainEvent(booking.Id));

        apartment.LastBookedOnUtc = utcNow;

        return booking;
    }

    /// <summary>
    ///     Confirms the booking if it is in the reserved status.
    /// </summary>
    /// <param name="utcNow">The current date and time in UTC.</param>
    /// <returns>A <see cref="Result" /> indicating success or failure.</returns>
    public Result Confirm(DateTime utcNow)
    {
        if (Status != BookingStatus.Reserved)
            return Result.Failure(BookingErrors.NotReserved);

        Status = BookingStatus.Confirmed;
        ConfirmedOnUtc = utcNow;

        RaiseDomainEvent(new BookingConfirmedDomainEvent(Id));

        return Result.Success();
    }

    /// <summary>
    ///     Rejects the booking if it is in the reserved status.
    /// </summary>
    /// <param name="utcNow">The current date and time in UTC.</param>
    /// <returns>A <see cref="Result" /> indicating success or failure.</returns>
    public Result Reject(DateTime utcNow)
    {
        if (Status != BookingStatus.Reserved)
            return Result.Failure(BookingErrors.NotReserved);

        Status = BookingStatus.Rejected;
        RejectedOnUtc = utcNow;

        RaiseDomainEvent(new BookingRejectedDomainEvent(Id));

        return Result.Success();
    }

    /// <summary>
    ///     Completes the booking if it is in the confirmed status.
    /// </summary>
    /// <param name="utcNow">The current date and time in UTC.</param>
    /// <returns>A <see cref="Result" /> indicating success or failure.</returns>
    public Result Complete(DateTime utcNow)
    {
        if (Status != BookingStatus.Confirmed)
            return Result.Failure(BookingErrors.NotConfirmed);

        Status = BookingStatus.Completed;
        RejectedOnUtc = utcNow;

        RaiseDomainEvent(new BookingCompletedDomainEvent(Id));

        return Result.Success();
    }

    /// <summary>
    ///     Cancels the booking if it is in the confirmed status and has not started.
    /// </summary>
    /// <param name="utcNow">The current date and time in UTC.</param>
    /// <returns>A <see cref="Result" /> indicating success or failure.</returns>
    public Result Cancel(DateTime utcNow)
    {
        if (Status != BookingStatus.Confirmed)
            return Result.Failure(BookingErrors.NotConfirmed);

        var currenDate = DateOnly.FromDateTime(utcNow);

        if (currenDate > Duration.Start)
            return Result.Failure(BookingErrors.AlreadyStarted);

        Status = BookingStatus.Cancelled;
        CancelledOnUtc = utcNow;

        RaiseDomainEvent(new BookingCancelledDomainEvent(Id));

        return Result.Success();
    }
}