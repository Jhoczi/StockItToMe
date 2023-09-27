using MediatR;
using StockItToMe.Core.Commands;

namespace StockItToMe.Warehouse.Api.Commands;

public class AddNewProductCommand : BaseCommand, IRequest<MessageResult<string>>
{
    
}