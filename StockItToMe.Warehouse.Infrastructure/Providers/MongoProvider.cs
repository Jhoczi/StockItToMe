using System.Linq.Expressions;
using MongoDB.Driver;
using StockItToMe.Core.Entities;

namespace StockItToMe.Warehouse.Infrastructure.Providers;

public class MongoProvider<TEntity> : IQueryMongoProvider<TEntity> where TEntity : IEntity
{
    private readonly IMongoDatabase _db;
    private readonly IMongoCollection<TEntity> _collection;

    public MongoProvider(IMongoClient mongoClient, string databaseName, string collectionName = "")
    {
        _db = mongoClient.GetDatabase(databaseName);
        
        if (string.IsNullOrEmpty(collectionName))
            collectionName= typeof(TEntity).Name;
        
        _collection = _db.GetCollection<TEntity>(collectionName);
    }
    
    public async Task<TEntity> GetSingle(object id)
    {
        return await GetSingle( x => x.Id.Equals(id));
    }

    public async Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> criteria)
    {
        //return (await _collection.FindAsync(criteria)).FirstOrDefault();
        return (await Find(criteria)).FirstOrDefault();
    }

    public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter)
    {
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> Find(FilterDefinition<TEntity> filter)
    {
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter, int page, int pageSize)
    {
        if (page < 1)
            throw new ArgumentException("Page index must be at least 1", nameof(page));
        
        return await _collection.Find(filter).Skip(page-1).Limit(pageSize).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> Find(FilterDefinition<TEntity> filter, int page, int pageSize)
    {
        if (page < 1)
            throw new ArgumentException("Page index must be at least 1", nameof(page));
        
        return await _collection.Find(filter).Skip(page-1).Limit(pageSize).ToListAsync();
    }
}