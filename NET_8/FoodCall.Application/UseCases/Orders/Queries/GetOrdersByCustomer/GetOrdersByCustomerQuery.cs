using FoodCall.Application.DTOs;
using MediatR;

namespace FoodCall.Application.UseCases.Orders.Queries.GetOrdersByCustomer;

public record GetOrdersByCustomerQuery(Guid CustomerId) : IRequest<List<OrderDto>>;
