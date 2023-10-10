using MassTransit;
using StockItToMe.Core.Entities;
using StockItToMe.Core.Events;
using StockItToMe.Core.Messages;

namespace StockItToMe.Consumer.EventsConsumer.Consumers;

public class WarehouseApiConsumer : IConsumer<EventMessage>
{
    private readonly ILogger<WarehouseApiConsumer> _logger;
    private readonly ICommandDataProvider<DomainEvent> _dataProvider;

    public WarehouseApiConsumer(ICommandDataProvider<DomainEvent> dataProvider, ILogger<WarehouseApiConsumer> logger)
    {
        _dataProvider = dataProvider;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<EventMessage> context)
    {
        var message = context.Message;
        _logger.LogInformation($"[INFO]: Starting consuming message with id:{message.Id}");
        
        await _dataProvider.Create(new DomainEvent
        {
            Id = message.Id,
            AggregateId = message.AggregateId,
            EventType = message.EventType,
            Version = message.Version,
            TimeStamp = message.TimeStamp,
            Payload = message.Payload
        });
        
        _logger.LogInformation($"[INFO]: Finished consuming message with id:{message.Id}");
    }
}