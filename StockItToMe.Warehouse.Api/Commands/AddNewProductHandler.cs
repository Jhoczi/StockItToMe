using System.Text.Json;
using MassTransit;
using MediatR;
using StockItToMe.Common.Events;
using StockItToMe.Core.Events;
using StockItToMe.Core.Producers;

namespace StockItToMe.Warehouse.Api.Commands;

public class AddNewProductHandler: IRequestHandler<AddNewProductCommand, MessageResult<string>>
{
    private readonly ILogger<AddNewProductHandler> _logger;
    private readonly IEventProducer _eventProducer;

    public AddNewProductHandler(ILogger<AddNewProductHandler> logger, IEventProducer eventProducer, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _eventProducer = eventProducer;
    }

    public async Task<MessageResult<string>> Handle(AddNewProductCommand request, CancellationToken cancellationToken)
    {
        var eventType = typeof(ProductCreatedEvent).AssemblyQualifiedName;

        if (string.IsNullOrWhiteSpace(eventType))
            throw new ArgumentNullException(nameof(ProductCreatedEvent), "The specified type is not found");

        var eventData = new BaseEvent()
        {
            Id = request.Id,
            EventType = typeof(ProductCreatedEvent).AssemblyQualifiedName,
            Payload = JsonSerializer.Serialize(request),
        };
        
        await _eventProducer.Produce(eventData, cancellationToken);
        
        return new MessageResult<string>(true, eventData.Id.ToString());
    }
}