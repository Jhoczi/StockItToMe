using MassTransit;
using StockItToMe.Core.Entities;
using StockItToMe.Core.Events;

namespace StockItToMe.Consumer.CommandWarehouseApiConsumer.Consumers;

public class WarehouseApiConsumer : IConsumer<BaseEvent>
{
    private readonly ILogger<WarehouseApiConsumer> _logger;
    private readonly ICommandDataProvider<BaseEvent> _dataProvider;

    public WarehouseApiConsumer(ICommandDataProvider<BaseEvent> dataProvider, ILogger<WarehouseApiConsumer> logger)
    {
        _dataProvider = dataProvider;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<BaseEvent> context)
    {
        _logger.LogInformation($"Starting consuming message:{context.Message.Id}");
        await _dataProvider.Create(context.Message);
        _logger.LogInformation($"Finished consuming message:{context.Message.Id}");
    }
}