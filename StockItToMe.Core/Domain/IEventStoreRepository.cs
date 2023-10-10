using StockItToMe.Core.Events;

namespace StockItToMe.Core.Domain;

public interface IEventStoreRepository
{
    Task SaveAsync(DomainEvent domainEvent);
    Task<List<DomainEvent>> FindByAggregateId(Guid aggregateId);
    Task<List<DomainEvent>> FindAllAsync();
}