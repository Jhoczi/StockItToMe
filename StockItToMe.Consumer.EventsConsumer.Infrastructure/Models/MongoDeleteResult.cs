using MongoDB.Driver;
using StockItToMe.Core.Entities;

namespace StockItToMe.Consumer.EventsConsumer.Infrastructure.Models;

public class MongoDeleteResult : IDeleteResult
{
    public long DeletedCount { get => _mongoDeleteResult.DeletedCount;}
    public bool IsAcknowledged { get => _mongoDeleteResult.IsAcknowledged; }

    private readonly DeleteResult _mongoDeleteResult;

    public MongoDeleteResult(DeleteResult mongoDeleteResult)
    {
        _mongoDeleteResult = mongoDeleteResult;
    }
}