using FoodCall.Application.DTOs;
using MediatR;

namespace FoodCall.Application.UseCases.Users.Commands.CreateUser;

public record CreateUserCommand(CreateUserDto User) : IRequest<UserDto>;
