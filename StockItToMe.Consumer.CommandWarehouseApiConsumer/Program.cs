using System.Reflection;
using System.Text.Json;
using MassTransit;
using StockItToMe.Common.Events;
using StockItToMe.Core.Events;

var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.Host("localhost", "/", h =>
    {
        h.Username("user");
        h.Password("password");
    });

    cfg.ReceiveEndpoint("warehouse-api-events", e =>
    {
        e.Consumer<WarehouseApiConsumer>();
    });
});

await busControl.StartAsync(); // Start the bus
Console.WriteLine("Press any key to exit");
await Task.Run(() => Console.ReadKey());
await busControl.StopAsync(); // Stop the bus

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





