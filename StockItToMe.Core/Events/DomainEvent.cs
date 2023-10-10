using System.Data;
using StockItToMe.Core.Entities;
using StockItToMe.Warehouse.Api.Handlers;

namespace StockItToMe.Core.Events;

public class DomainEvent : IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid AggregateId { get; init; }
    public int Version { get; set; }
    public DateTime TimeStamp { get; init; } = DateTime.UtcNow;
    public EventType EventType { get; init; }
    public string? Payload { get; init; }
}
