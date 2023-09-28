using MassTransit;
using StockItToMe.Core.Events;
using StockItToMe.Core.Producers;

namespace StockItToMe.Warehouse.Infrastructure.Producers;

public class EventProducer : IEventProducer
{
    private readonly IPublishEndpoint _publishEndpoint;

    public EventProducer(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task Produce(EventModel eventModelData, CancellationToken cancellationToken)
    {
        await _publishEndpoint.Publish(eventModelData, cancellationToken);
    }
}