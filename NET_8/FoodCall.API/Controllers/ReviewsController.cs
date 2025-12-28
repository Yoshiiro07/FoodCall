using FoodCall.Application.DTOs;
using FoodCall.Application.UseCases.Reviews.Commands.CreateReview;
using FoodCall.Application.UseCases.Reviews.Queries.GetReviewsByRestaurant;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodCall.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReviewsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<ReviewDto>> CreateReview([FromBody] CreateReviewDto dto)
    {
        var result = await _mediator.Send(new CreateReviewCommand(dto));
        return CreatedAtAction(nameof(GetReviewsByRestaurant), new { restaurantId = result.OrderId }, result);
    }

    [HttpGet("restaurant/{restaurantId:guid}")]
    public async Task<ActionResult<List<ReviewDto>>> GetReviewsByRestaurant(Guid restaurantId)
    {
        var result = await _mediator.Send(new GetReviewsByRestaurantQuery(restaurantId));
        return Ok(result);
    }
}
