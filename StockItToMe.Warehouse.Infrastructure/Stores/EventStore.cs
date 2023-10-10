using MassTransit;
using StockItToMe.Core.Domain;
using StockItToMe.Core.Events;
using StockItToMe.Core.Exceptions;

namespace StockItToMe.Warehouse.Infrastructure.Stores;

public class EventStore : IEventStore
{
    private readonly IEventStoreRepository _eventStoreRepository;

    public EventStore(IEventStoreRepository eventStoreRepository)
    {
        _eventStoreRepository = eventStoreRepository;
    }

    public async Task SaveEvents(Guid aggregateId, IEnumerable<DomainEvent> events, int expectedVersion)
    {
        var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);

        if (expectedVersion != -1 && eventStream[^1].Version != expectedVersion)
        {
            throw new ConcurrencyException();
        }

        var version = expectedVersion;

        foreach (var eventModel in events)
        {
            version++;
            eventModel.Version = version;
            
            await _eventStoreRepository.SaveAsync(eventModel);
        }
    }

    public async Task<List<DomainEvent>> GetEvents(Guid aggregateId)
    {
        var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);
        
        if (eventStream == null || !eventStream.Any())
        {
            throw new AggregateNotFoundException("Incorrect post ID provided.");
        }

        return eventStream.OrderBy(x => x.Version).ToList();
    }

    public async Task<List<Guid>> GetAggregateIds(Guid aggregateId)
    {
        var eventStream = await _eventStoreRepository.FindAllAsync();

        if (eventStream == null || !eventStream.Any())
        {
            throw new ArgumentNullException(nameof(eventStream),"Could not retrieve event stream from the event store.");
        }

        return eventStream.Select(x => x.AggregateId).Distinct().ToList();
    }
}