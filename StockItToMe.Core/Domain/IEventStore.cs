using StockItToMe.Core.Events;

namespace StockItToMe.Core.Domain;

public interface IEventStore
{
    Task SaveEvents(Guid aggregateId, IEnumerable<DomainEvent> events, int expectedVersion);
    Task<List<DomainEvent>> GetEvents(Guid aggregateId);
    Task<List<Guid>> GetAggregateIds(Guid aggregateId);
}