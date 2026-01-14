using FoodCall.Application.DTOs;
using MediatR;

namespace FoodCall.Application.UseCases.Auth.Commands.Register;

public record RegisterCommand(
    string Name,
    string Email,
    string Phone,
    string Password
) : IRequest<UserDto>;
