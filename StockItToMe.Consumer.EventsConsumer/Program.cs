using MassTransit;
using MongoDB.Driver;
using StockItToMe.Consumer.EventsConsumer.Consumers;
using StockItToMe.Consumer.EventsConsumer.Infrastructure;
using StockItToMe.Core.Entities;
using StockItToMe.Core.Events;

var builder = Host.CreateDefaultBuilder();

builder.ConfigureAppConfiguration((hostingContext, configuration) =>
{
    configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
});
builder.ConfigureServices((hostingContext, services) =>
{
    services.AddSingleton<IMongoClient>(_ =>
    {
        var mongoClient = new MongoClient(
            hostingContext.Configuration.GetSection("MongoProviderSettings").GetSection("ConnectionStrings")["Warehouse"]);
        return mongoClient;
    });
    services.AddSingleton<ICommandDataProvider<DomainEvent>, MongoProvider<DomainEvent>>(provider =>
        new MongoProvider<DomainEvent>(
            provider.GetRequiredService<IMongoClient>(),
            hostingContext.Configuration.GetSection("MongoProviderSettings").GetSection("DatabaseNames")["Warehouse"],
            nameof(DomainEvent)
        ));
    
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
        
            cfg.ReceiveEndpoint("warehouse-api-events-messages", e =>
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