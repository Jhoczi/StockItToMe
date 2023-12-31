using MassTransit;
using MongoDB.Driver;
using Serilog;
using StockItToMe.Core.Domain;
using StockItToMe.Core.Entities;
using StockItToMe.Core.Events;
using StockItToMe.Core.Producers;
using StockItToMe.Warehouse.Api.Middlewares;
using StockItToMe.Warehouse.Infrastructure.Producers;
using StockItToMe.Warehouse.Infrastructure.Providers;
using StockItToMe.Warehouse.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog((hostingContext, loggerConfiguration) => 
{
    loggerConfiguration
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console();
});

//builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection(nameof(KafkaSettings)));

builder.Services.AddSingleton<IMongoClient>(_ =>
{
    var mongoClient = new MongoClient(
        builder.Configuration.GetSection("MongoProviderSettings").GetSection("ConnectionStrings")["Warehouse"]);
    return mongoClient;
});
builder.Services.AddSingleton<IQueryDataProvider<DomainEvent>, MongoProvider<DomainEvent>>(provider =>
    new MongoProvider<DomainEvent>(
        provider.GetRequiredService<IMongoClient>(),
        builder.Configuration.GetSection("MongoProviderSettings").GetSection("DatabaseNames")["Warehouse"],
        nameof(DomainEvent)
    ));

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost","/", h =>
        {
            h.Username("user");
            h.Password("password");
        });
        
        cfg.ConfigureEndpoints(context);
        
        // cfg.Publish<BaseEvent>(y => 
        // {
        //     y.Durable = true;
        // });
        
        // cfg.ReceiveEndpoint("warehouse-api-events", e =>
        // {
        //     e.Bind<BaseEvent>();
        // });
        
        // cfg.ReceiveEndpoint("product-created-queue", e =>
        // {
        //     //e.Bind<BaseEvent>();
        // });
    });
    
    // x.UsingInMemory((context, cfg) => { cfg.ConfigureEndpoints(context); });
    //
    // x.AddRider(rider =>
    // {
    //     var kafkaSettings = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<KafkaSettings>>().Value;
    //     
    //     rider.AddProducer<BaseEvent>(kafkaSettings.ProducerTopic);
    //     
    //     rider.UsingKafka((_, kafkaConfiguration) =>
    //     {
    //         kafkaConfiguration.Host(kafkaSettings.Host);
    //     });
    // });
});

builder.Services.AddTransient<IEventStoreRepository, EventStoreRepository>();
builder.Services.AddTransient<IEventProducer, EventProducer>();

builder.Services.AddControllers();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();

app.Run();