using FoodCall.Application.DTOs;
using FoodCall.Application.UseCases.Orders.Commands.CreateOrder;
using FoodCall.Application.UseCases.Orders.Commands.ConfirmOrder;
using FoodCall.Application.UseCases.Orders.Commands.CancelOrder;
using FoodCall.Application.UseCases.Orders.Queries.GetOrderById;
using FoodCall.Application.UseCases.Orders.Queries.GetOrdersByCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodCall.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] CreateOrderDto dto)
    {
        var result = await _mediator.Send(new CreateOrderCommand(dto));
        return CreatedAtAction(nameof(GetOrderById), new { id = result.Id }, result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderDto>> GetOrderById(Guid id)
    {
        var result = await _mediator.Send(new GetOrderByIdQuery(id));
        return Ok(result);
    }

    [HttpGet("customer/{customerId:guid}")]
    public async Task<ActionResult<List<OrderDto>>> GetOrdersByCustomer(Guid customerId)
    {
        var result = await _mediator.Send(new GetOrdersByCustomerQuery(customerId));
        return Ok(result);
    }

    [HttpPut("{id:guid}/confirm")]
    public async Task<IActionResult> ConfirmOrder(Guid id)
    {
        await _mediator.Send(new ConfirmOrderCommand(id));
        return NoContent();
    }

    [HttpPut("{id:guid}/cancel")]
    public async Task<IActionResult> CancelOrder(Guid id)
    {
        await _mediator.Send(new CancelOrderCommand(id));
        return NoContent();
    }
}
