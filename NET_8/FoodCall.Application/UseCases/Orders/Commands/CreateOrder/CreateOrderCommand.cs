using FoodCall.Application.DTOs;
using MediatR;

namespace FoodCall.Application.UseCases.Orders.Commands.CreateOrder;

public record CreateOrderCommand(CreateOrderDto Order) : IRequest<OrderDto>;
