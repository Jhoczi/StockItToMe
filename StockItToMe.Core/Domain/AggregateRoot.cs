using StockItToMe.Core.Events;

namespace StockItToMe.Core.Domain;

public abstract class AggregateRoot
{
    protected Guid _id;
    private readonly List<EventModel> _changes = new();
    public Guid Id => _id;
    public int Version { get; set; } = -1;

    public IEnumerable<EventModel> GetUncomittedChanges() => _changes;

    public void MarkChangesAsComitted()
    {
        _changes.Clear();
    }

    private void ApplyChanges(EventModel eventModel, bool isNew)
    {
        
        
        if (isNew)
            _changes.Add(eventModel);
    }

    protected void RaiseEvent(EventModel eventModel)
    {
        ApplyChanges(eventModel, true);
    }

    protected void ReplayEvents(IEnumerable<EventModel> events)
    {
        foreach (var eventModel in events)
        {
            ApplyChanges(eventModel, false);
        }
    }
}