using System.Net;
using System.Text.Json;
using StockItToMe.Core.Models;

namespace StockItToMe.Warehouse.Api.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    
    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            HandleException(context, ex);
        }
    }
    
    private static void HandleException(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var errorResponse = new ErrorDetails
        {
            Message = $"There was an error durring operation: ${exception.Message}"
        };

        switch(exception)
        {
            case InvalidOperationException e:
                errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            case KeyNotFoundException e:
                errorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            default:
                errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        var json = JsonSerializer.Serialize(errorResponse);
        context.Response.WriteAsync(json);
    }
}