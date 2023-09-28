using StockItToMe.Core.Entities;

namespace StockItToMe.Core.Events;

public class EventModel : IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid AggregateId { get; init; }
    public int Version { get; set; }
    public DateTime TimeStamp { get; init; } = DateTime.UtcNow;
    public string AggregateType { get; init; }
    public string EventType { get; init; }
    public string Payload { get; init; }
}
