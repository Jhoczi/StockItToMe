using MassTransit;
using StockItToMe.Core.Events;
using StockItToMe.Core.Producers;

namespace StockItToMe.Warehouse.Infrastracture.Producers;

public class EventProducer : IEventProducer
{
    private readonly IPublishEndpoint _publishEndpoint;

    public EventProducer(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task Produce(BaseEvent eventData, CancellationToken cancellationToken)
    {
        await _publishEndpoint.Publish(eventData, cancellationToken);
    }
}