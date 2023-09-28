using StockItToMe.Core.Events;

namespace StockItToMe.Core.Domain;

public interface IEventStore
{
    Task SaveEvents(Guid aggregateId, IEnumerable<EventModel> events, int expectedVersion);
    Task<List<EventModel>> GetEvents(Guid aggregateId);
    Task<List<Guid>> GetAggregateIds(Guid aggregateId);
}