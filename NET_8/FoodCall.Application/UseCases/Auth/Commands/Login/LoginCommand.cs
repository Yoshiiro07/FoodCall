using FoodCall.Application.DTOs;
using MediatR;

namespace FoodCall.Application.UseCases.Auth.Commands.Login;

public record LoginCommand(string Email, string Password) : IRequest<LoginResponse>;
