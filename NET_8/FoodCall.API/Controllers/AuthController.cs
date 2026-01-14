using FoodCall.Application.DTOs;
using FoodCall.Application.UseCases.Auth.Commands.Login;
using FoodCall.Application.UseCases.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodCall.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        var command = new LoginCommand(request.Email, request.Password);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<UserDto>> Register([FromBody] RegisterRequest request)
    {
        var command = new RegisterCommand(
            request.Name,
            request.Email,
            request.Phone,
            request.Password
        );
        
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(Register), new { id = result.Id }, result);
    }
}
