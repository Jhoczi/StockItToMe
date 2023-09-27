namespace StockItToMe.Core.Events;

public class BaseEvent
{
    public Guid Id { get; init; }
    public Guid AggregateId { get; init; }
    public int Version { get; init; }
    public DateTime TimeStamp { get; init; } = DateTime.UtcNow;
    public string AggregateType { get; init; }
    public string EventType { get; init; }
    public string Payload { get; init; }
}
