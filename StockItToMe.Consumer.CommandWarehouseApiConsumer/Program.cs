using MassTransit;
using StockItToMe.Consumer.CommandWarehouseApiConsumer.Consumers;

var builder = Host.CreateDefaultBuilder();

builder.ConfigureServices(services =>
{
    services.AddMassTransit(x =>
    {
        x.AddConsumer<WarehouseApiConsumer>();
    
        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host("localhost", "/", h =>
            {
                h.Username("user");
                h.Password("password");
            });
        
            cfg.ReceiveEndpoint("warehouse-api-events", e =>
            {
                e.ConfigureConsumer<WarehouseApiConsumer>(context);
            });
        });
    });
});

// var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
// {
//     cfg.Host("localhost", "/", h =>
//     {
//         h.Username("user");
//         h.Password("password");
//     });
//
//     cfg.ReceiveEndpoint("warehouse-api-events", e =>
//     {
//         e.Consumer<WarehouseApiConsumer>();
//     });
// });
//
// await busControl.StartAsync(); // Start the bus
// Console.WriteLine("Press any key to exit");
// await Task.Run(() => Console.ReadKey());
// await busControl.StopAsync(); // Stop the bus

var app = builder.Build();

await app.RunAsync();
Console.WriteLine("Application started");