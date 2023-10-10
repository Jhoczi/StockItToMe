using System.Linq.Expressions;

namespace StockItToMe.Core.Entities;

public interface IQueryDataProvider<TEntity> where TEntity : IEntity
{
    Task<TEntity> GetSingle(object id);
    Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> criteria);
}