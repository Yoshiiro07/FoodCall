using FoodCall.Application.DTOs;
using FoodCall.Application.UseCases.Users.Commands.CreateUser;
using FoodCall.Application.UseCases.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodCall.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto dto)
    {
        var result = await _mediator.Send(new CreateUserCommand(dto));
        return CreatedAtAction(nameof(GetUserById), new { id = result.Id }, result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDto>> GetUserById(Guid id)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(id));
        return Ok(result);
    }
}
