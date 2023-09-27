using System.Linq.Expressions;

namespace StockItToMe.Core.Entities;

public interface ICommandDataProvider<TEntity> where TEntity : IEntity
{
    Task<TEntity> Create(TEntity entity);
    Task<TEntity> Update(TEntity entity);
    Task<IDeleteResult> Delete(object id);
    Task<IDeleteResult> Delete(Expression<Func<TEntity, bool>> criteria);
}