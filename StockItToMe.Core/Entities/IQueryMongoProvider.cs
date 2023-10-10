using System.Linq.Expressions;
using MongoDB.Driver;

namespace StockItToMe.Core.Entities;

public interface IQueryMongoProvider<TEntity> : IQueryDataProvider<TEntity> where TEntity : IEntity
{
    Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter, int page, int pageSize);
    Task<IEnumerable<TEntity>> Find(FilterDefinition<TEntity> filter);
    Task<IEnumerable<TEntity>> Find(FilterDefinition<TEntity> filter, int page, int pageSize);
}