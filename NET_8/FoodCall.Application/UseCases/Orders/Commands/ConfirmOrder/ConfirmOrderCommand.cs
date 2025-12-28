using MediatR;

namespace FoodCall.Application.UseCases.Orders.Commands.ConfirmOrder;

public record ConfirmOrderCommand(Guid OrderId) : IRequest<bool>;
