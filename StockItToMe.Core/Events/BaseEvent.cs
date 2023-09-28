using StockItToMe.Core.Entities;

namespace StockItToMe.Core.Events;

public class BaseEvent : IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid AggregateId { get; init; }
    public int Version { get; init; }
    public DateTime TimeStamp { get; init; } = DateTime.UtcNow;
    public string AggregateType { get; init; }
    public string EventType { get; init; }
    public string Payload { get; init; }
}
