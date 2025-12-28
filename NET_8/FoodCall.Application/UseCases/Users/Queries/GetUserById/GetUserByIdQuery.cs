using FoodCall.Application.DTOs;
using MediatR;

namespace FoodCall.Application.UseCases.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<UserDto>;
