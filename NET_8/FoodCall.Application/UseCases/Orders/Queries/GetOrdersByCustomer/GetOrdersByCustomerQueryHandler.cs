using FoodCall.Application.DTOs;
using FoodCall.Domain.Repositories;
using MediatR;

namespace FoodCall.Application.UseCases.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerQueryHandler : IRequestHandler<GetOrdersByCustomerQuery, List<OrderDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrdersByCustomerQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<OrderDto>> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.Orders.GetByCustomerIdAsync(request.CustomerId);

        return orders.Select(o => new OrderDto(
            o.Id,
            o.CustomerId,
            o.RestaurantId,
            o.TotalValue,
            o.Status,
            o.Items.Select(i => new OrderItemDto(
                i.Id,
                i.ProductId,
                i.ProductName,
                i.UnitPrice,
                i.Quantity,
                i.SubTotal
            )).ToList()
        )).ToList();
    }
}
