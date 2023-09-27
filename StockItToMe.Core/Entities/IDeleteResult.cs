namespace StockItToMe.Core.Entities;

public interface IDeleteResult
{
    long DeletedCount { get; }
    bool IsAcknowledged { get; }
}