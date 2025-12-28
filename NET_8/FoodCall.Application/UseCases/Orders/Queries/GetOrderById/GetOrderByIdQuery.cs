using FoodCall.Application.DTOs;
using MediatR;

namespace FoodCall.Application.UseCases.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(Guid OrderId) : IRequest<OrderDto>;
