using FoodCall.Application.DTOs;
using FoodCall.Application.UseCases.Restaurants.Commands.CreateRestaurant;
using FoodCall.Application.UseCases.Restaurants.Queries.GetAllRestaurants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodCall.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RestaurantsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<RestaurantDto>> CreateRestaurant([FromBody] CreateRestaurantDto dto)
    {
        var result = await _mediator.Send(new CreateRestaurantCommand(dto));
        return CreatedAtAction(nameof(GetAllRestaurants), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<ActionResult<List<RestaurantDto>>> GetAllRestaurants()
    {
        var result = await _mediator.Send(new GetAllRestaurantsQuery());
        return Ok(result);
    }
}
