using System.Text.Json;
using MassTransit;
using StockItToMe.Common.Events;
using StockItToMe.Core.Events;

namespace StockItToMe.Consumer.CommandWarehouseApiConsumer.Consumers;

public class WarehouseApiConsumer : IConsumer<BaseEvent>
{
    public Task Consume(ConsumeContext<BaseEvent> context)
    {
        var eventType = Type.GetType(context.Message.EventType);

        if (eventType == null)
            throw new ArgumentNullException(nameof(context.Message.EventType),$"The given type is not supported.");
        
        var eventData = JsonSerializer.Deserialize(context.Message.Payload, eventType);

        switch (eventData)
        {
            case ProductCreatedEvent:
                Console.WriteLine("TUTAJ MAPUJEMY DO MONGO DLA utworzonego");
                break;
            default:
                Console.WriteLine("Zaden z tych typow");
                break;
        }
        
        return Task.CompletedTask;
    }
}