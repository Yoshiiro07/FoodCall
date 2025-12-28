using FoodCall.Application.DTOs;
using FoodCall.Application.UseCases.Products.Commands.CreateProduct;
using FoodCall.Application.UseCases.Products.Queries.GetProductsByRestaurant;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodCall.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductDto dto)
    {
        var result = await _mediator.Send(new CreateProductCommand(dto));
        return CreatedAtAction(nameof(GetProductsByRestaurant), new { restaurantId = result.RestaurantId }, result);
    }

    [HttpGet("restaurant/{restaurantId:guid}")]
    public async Task<ActionResult<List<ProductDto>>> GetProductsByRestaurant(Guid restaurantId)
    {
        var result = await _mediator.Send(new GetProductsByRestaurantQuery(restaurantId));
        return Ok(result);
    }
}
