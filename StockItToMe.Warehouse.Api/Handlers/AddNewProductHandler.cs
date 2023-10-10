using System.Text.Json;
using MassTransit;
using MediatR;
using StockItToMe.Core.Domain;
using StockItToMe.Core.Events;
using StockItToMe.Core.Messages;
using StockItToMe.Core.Producers;
using StockItToMe.Warehouse.Api.Commands;

namespace StockItToMe.Warehouse.Api.Handlers;

public class AddNewProductHandler: IRequestHandler<AddNewProductCommand, CommandResult<string>>
{
    private readonly ILogger<AddNewProductHandler> _logger;
    private readonly IEventStoreRepository _eventStoreRepository;
    private readonly IEventProducer _eventProducer;

    public AddNewProductHandler(ILogger<AddNewProductHandler> logger, IEventProducer eventProducer, IPublishEndpoint publishEndpoint, IEventStoreRepository eventStoreRepository)
    {
        _logger = logger;
        _eventProducer = eventProducer;
        _eventStoreRepository = eventStoreRepository;
    }

    public async Task<CommandResult<string>> Handle(AddNewProductCommand request, CancellationToken cancellationToken)
    {
        //var eventType = typeof(ProductCreatedDomainEvent).AssemblyQualifiedName;

        // if (string.IsNullOrWhiteSpace(request.))
        //     throw new ArgumentNullException(nameof(ProductCreatedDomainEvent), "The specified type is not found");
        
        var eventData = new EventMessage()
        {
            Id = request.Id,
            AggregateId = Guid.NewGuid(),
            EventType = EventType.ProductCreated,
            Payload = JsonSerializer.Serialize(request),
        };
        
        await _eventProducer.Produce(eventData, cancellationToken);
        
        return new CommandResult<string>(true, eventData.Id.ToString());
    }
}