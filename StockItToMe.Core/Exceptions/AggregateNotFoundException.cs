using StockItToMe.Core.Commands;

namespace StockItToMe.Core.Exceptions;

public class AggregateNotFoundException : Exception
{
    public AggregateNotFoundException(string message) : base(message)
    {
        
    }
}