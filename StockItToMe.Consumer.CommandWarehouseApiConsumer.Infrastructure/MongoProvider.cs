using System.Linq.Expressions;
using MongoDB.Driver;
using StockItToMe.Consumer.CommandWarehouseApiConsumer.Infrastructure.Models;
using StockItToMe.Core.Entities;

namespace StockItToMe.Consumer.CommandWarehouseApiConsumer.Infrastructure;

public class MongoProvider<TEntity> : ICommandDataProvider<TEntity> where TEntity : IEntity
{
    private readonly IMongoDatabase _db;
    private readonly IMongoCollection<TEntity> _collection;

    public MongoProvider(IMongoClient mongoClient, string databaseName, string collectionName)
    {
        _db = mongoClient.GetDatabase(databaseName);
        
        if (string.IsNullOrEmpty(collectionName))
            collectionName= typeof(TEntity).Name;
        
        _collection = _db.GetCollection<TEntity>(collectionName);
    }
    
    public async Task<TEntity> Create(TEntity entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task<TEntity> Update(TEntity entity)
    {
        var result = await _collection.ReplaceOneAsync(
            x => x.Id.Equals(entity.Id), entity,
            new ReplaceOptions { IsUpsert = false });

        if (result.MatchedCount != 1)
        {
            throw new KeyNotFoundException($"Updating {_collection.CollectionNamespace} failed for id = {entity.Id}");
        }

        return entity;
    }

    public async Task<IDeleteResult> Delete(object id)
    {
        return await Delete(x => x.Id.Equals(id));
    }

    public async Task<IDeleteResult> Delete(Expression<Func<TEntity, bool>> criteria)
    {
        return new MongoDeleteResult(await _collection.DeleteOneAsync(criteria));
    }
}