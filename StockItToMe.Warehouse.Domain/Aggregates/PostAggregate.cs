using StockItToMe.Core.Domain;
using StockItToMe.Core.Events;

namespace StickItToMe.Warehouse.Domain.Aggregates;

public class PostAggregate : AggregateRoot
{
    private bool _active;

    public PostAggregate()
    {
        
    }

    public void Apply(DomainEvent domainEvent)
    {
        RaiseEvent(new()
        {
            Id = _id,
            
        });
    }
}