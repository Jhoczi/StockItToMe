namespace StockItToMe.Consumer.CommandWarehouseApiConsumer.Infrastructure;

public interface IMongoProviderSettings
{
    public Dictionary<string, string> DatabaseNames { get; set; }
    public Dictionary<string, string> ConnectionStrings { get; set; }
}