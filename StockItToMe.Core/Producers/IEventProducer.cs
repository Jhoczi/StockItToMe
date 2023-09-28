using StockItToMe.Core.Events;

namespace StockItToMe.Core.Producers;

public interface IEventProducer
{
    Task Produce(EventModel eventModelData, CancellationToken cancellationToken);
}