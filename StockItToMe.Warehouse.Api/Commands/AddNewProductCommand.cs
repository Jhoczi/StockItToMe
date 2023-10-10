using MediatR;

namespace StockItToMe.Warehouse.Api.Commands;

public class AddNewProductCommand : IRequest<CommandResult<string>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid ProducerId { get; set; }
    public string ProducerName { get; set; }
    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    
}