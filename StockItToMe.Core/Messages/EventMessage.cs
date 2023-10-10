using StockItToMe.Warehouse.Api.Handlers;

namespace StockItToMe.Core.Messages;

public record EventMessage
{
    public Guid Id { get; init; }
    public Guid AggregateId { get; init; }
    public int Version { get; set; }
    public DateTime TimeStamp { get; init; } = DateTime.UtcNow;
    public EventType EventType { get; init; }
    public string? Payload { get; init; }
}