using System.Security.AccessControl;
using StockItToMe.Core.Events;

namespace StockItToMe.Common.Events;

public class ProductCreatedEvent : BaseEvent
{
    // public string Author { get; set; }
    // // public ProductCreatedEvent() : base(nameof(ProductCreatedEvent))
    // // {
    // // }
}

public class ProductXDEvent : BaseEvent
{
    public string TestMessage { get; set; }
    // public ProductXDEvent() : base(nameof(ProductXDEvent))
    // {
    // }
}