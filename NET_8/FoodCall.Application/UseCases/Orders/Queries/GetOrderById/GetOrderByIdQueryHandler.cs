using FoodCall.Application.DTOs;
using FoodCall.Domain.Exceptions;
using FoodCall.Domain.Repositories;
using MediatR;

namespace FoodCall.Application.UseCases.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrderByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId);
        if (order == null)
            throw new EntityNotFoundException("Order", request.OrderId);

        var orderItemsDtos = order.Items.Select(i => new OrderItemDto(
            i.Id,
            i.ProductId,
            i.ProductName,
            i.UnitPrice,
            i.Quantity,
            i.SubTotal
        )).ToList();

        return new OrderDto(
            order.Id,
            order.CustomerId,
            order.RestaurantId,
            order.TotalValue,
            order.Status,
            orderItemsDtos
        );
    }
}
