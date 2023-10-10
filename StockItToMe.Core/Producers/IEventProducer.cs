using StockItToMe.Core.Events;
using StockItToMe.Core.Messages;

namespace StockItToMe.Core.Producers;

public interface IEventProducer
{
    Task Produce(EventMessage message, CancellationToken cancellationToken);
}