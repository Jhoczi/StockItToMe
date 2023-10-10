using StockItToMe.Core.Events;

namespace StockItToMe.Core.Domain;

public abstract class AggregateRoot
{
    protected Guid _id;
    private readonly List<DomainEvent> _changes = new();
    public Guid Id => _id;
    public int Version { get; set; } = -1;

    public IEnumerable<DomainEvent> GetUncommittedChanges() => _changes;

    public void MarkChangesAsCommitted()
    {
        _changes.Clear();
    }

    private void ApplyChanges(DomainEvent domainEvent, bool isNew)
    {
        // Use method for specific event type.
        var method = GetType().GetMethod("Apply", new Type[] { domainEvent.GetType() }) 
                     ?? throw new ArgumentNullException(nameof(domainEvent), $"The apply method was not found in aggregate root for {domainEvent.GetType().Name}");
        
        method.Invoke(this, new object[] { domainEvent });
        
        if (isNew)
            _changes.Add(domainEvent);
    }

    protected void RaiseEvent(DomainEvent domainEvent)
    {
        ApplyChanges(domainEvent, true);
    }

    protected void ReplayEvents(IEnumerable<DomainEvent> events)
    {
        foreach (var eventModel in events)
        {
            ApplyChanges(eventModel, false);
        }
    }
}