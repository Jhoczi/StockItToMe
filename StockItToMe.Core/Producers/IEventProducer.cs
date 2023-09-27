using StockItToMe.Core.Events;

namespace StockItToMe.Core.Producers;

public interface IEventProducer
{
    Task Produce(BaseEvent eventData, CancellationToken cancellationToken);
}