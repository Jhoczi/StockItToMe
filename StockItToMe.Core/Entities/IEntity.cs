namespace StockItToMe.Core.Entities;

public interface IEntity
{
    object Id { get; }
}

public interface IBaseEntity<TId> : IEntity
{
    new TId Id { get; }
}

public interface IEntity<TId> : IBaseEntity<TId>
{
    public abstract TId Id { get; set; }
    object IEntity.Id => Id;
}