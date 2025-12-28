using FoodCall.Application.DTOs;
using FoodCall.Application.UseCases.Couriers.Queries.GetAvailableCouriers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodCall.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CouriersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CouriersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("available")]
    public async Task<ActionResult<List<CourierDto>>> GetAvailableCouriers()
    {
        var result = await _mediator.Send(new GetAvailableCouriersQuery());
        return Ok(result);
    }
}
