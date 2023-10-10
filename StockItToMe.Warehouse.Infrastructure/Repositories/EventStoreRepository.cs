using StockItToMe.Core.Domain;
using StockItToMe.Core.Entities;
using StockItToMe.Core.Events;

namespace StockItToMe.Warehouse.Infrastructure.Repositories;

public class EventStoreRepository : IEventStoreRepository
{
    private IQueryDataProvider<DomainEvent> _dataProvider;

    public EventStoreRepository(IQueryDataProvider<DomainEvent> dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public async Task SaveAsync(DomainEvent domainEvent)
    {
        throw new NotImplementedException();
    }

    public async Task<List<DomainEvent>> FindByAggregateId(Guid aggregateId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<DomainEvent>> FindAllAsync()
    {
        throw new NotImplementedException();
    }
}