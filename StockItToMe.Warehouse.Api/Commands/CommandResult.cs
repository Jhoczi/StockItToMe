namespace StockItToMe.Warehouse.Api.Commands;

public record CommandResult<TType>(bool IsSuccess, TType? Data, string Message = "Operation finished with success") where TType : class
{

}