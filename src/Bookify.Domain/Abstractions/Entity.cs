namespace Bookify.Domain.Abstractions;

/// <summary>
///     Represents a base class for domain entities.
/// </summary>
public abstract class Entity(Guid id)
{
    private readonly List<IDomainEvent> _domainEvents = [];

    /// <summary>
    ///     Gets the unique identifier of the entity.
    /// </summary>
    public Guid Id { get; init; } = id;

    /// <summary>
    ///     Gets a read-only list of domain events associated with the entity.
    /// </summary>
    /// <returns>A list of <see cref="IDomainEvent" /> instances.</returns>
    public IReadOnlyList<IDomainEvent> GetDomainEvents()
        => _domainEvents.ToList();

    /// <summary>
    ///     Clears all domain events from the entity.
    /// </summary>
    public void ClearDomainEvents()
        => _domainEvents.Clear();

    /// <summary>
    ///     Raises a domain event by adding it to the internal list.
    /// </summary>
    /// <param name="domainEvent">The domain event to raise.</param>
    protected void RaiseDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);
}