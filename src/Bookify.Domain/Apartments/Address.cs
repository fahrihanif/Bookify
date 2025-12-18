namespace Bookify.Domain.Apartments;

public record Address(
    string Street,
    string State,
    string ZipCode,
    string City,
    string Country
);