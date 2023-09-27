using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockItToMe.Common.DTOs;
using StockItToMe.Warehouse.Api.Commands;

namespace StockItToMe.Warehouse.Api.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
public class ProductController : ControllerBase 
{
    private readonly ILogger<ProductController> _logger;
    private readonly IMediator _mediator;
    
    public ProductController(ILogger<ProductController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> CreateProduct(AddNewProductCommand command)
    {
        command.Id = Guid.NewGuid();
        
        var commandResult = await _mediator.Send(command);
        
        return StatusCode(StatusCodes.Status201Created, new CreateProductResponse()
        {
            Message = commandResult.Message
        });
    }
}

