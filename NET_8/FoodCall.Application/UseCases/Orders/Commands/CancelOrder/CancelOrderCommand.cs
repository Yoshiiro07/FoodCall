using MediatR;

namespace FoodCall.Application.UseCases.Orders.Commands.CancelOrder;

public record CancelOrderCommand(Guid OrderId) : IRequest<bool>;
