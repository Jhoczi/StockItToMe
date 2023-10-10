using MassTransit;
using StockItToMe.Core.Messages;
using StockItToMe.Core.Producers;

namespace StockItToMe.Warehouse.Infrastructure.Producers;

public class EventProducer : IEventProducer
{
    private readonly IPublishEndpoint _publishEndpoint;

    public EventProducer(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task Produce(EventMessage message, CancellationToken cancellationToken)
    {
        await _publishEndpoint.Publish(message, cancellationToken);
    }
}