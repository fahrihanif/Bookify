using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Apartments;

/// <summary>
///     Represents an apartment entity.
/// </summary>
public sealed class Apartment : Entity
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Apartment" /> class.
    /// </summary>
    /// <param name="id">The unique identifier of the apartment.</param>
    /// <param name="name">The name of the apartment.</param>
    /// <param name="description">The description of the apartment.</param>
    /// <param name="address">The address of the apartment.</param>
    /// <param name="price">The price per night.</param>
    /// <param name="cleaningFee">The cleaning fee.</param>
    /// <param name="amenities">The list of amenities.</param>
    public Apartment(
        Guid id,
        Name name,
        Description description,
        Address address,
        Money price,
        Money cleaningFee,
        List<Amenity> amenities)
        : base(id)
    {
        Name = name;
        Description = description;
        Address = address;
        Price = price;
        CleaningFee = cleaningFee;
        Amenities = amenities;
    }

    /// <summary>
    ///     Gets the name of the apartment.
    /// </summary>
    public Name Name { get; private set; }

    /// <summary>
    ///     Gets the description of the apartment.
    /// </summary>
    public Description Description { get; private set; }

    /// <summary>
    ///     Gets the address of the apartment.
    /// </summary>
    public Address Address { get; private set; }

    /// <summary>
    ///     Gets the price per night.
    /// </summary>
    public Money Price { get; private set; }

    /// <summary>
    ///     Gets the cleaning fee.
    /// </summary>
    public Money CleaningFee { get; private set; }

    /// <summary>
    ///     Gets or sets the date and time when the apartment was last booked, in UTC.
    /// </summary>
    public DateTime? LastBookedOnUtc { get; internal set; }

    /// <summary>
    ///     Gets the list of amenities.
    /// </summary>
    public List<Amenity> Amenities { get; private set; } = [];
}