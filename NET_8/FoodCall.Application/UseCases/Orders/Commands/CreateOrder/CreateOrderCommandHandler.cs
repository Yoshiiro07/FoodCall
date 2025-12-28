using FoodCall.Application.DTOs;
using FoodCall.Domain.Entities;
using FoodCall.Domain.Exceptions;
using FoodCall.Domain.Repositories;
using MediatR;

namespace FoodCall.Application.UseCases.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Users.GetByIdAsync(request.Order.CustomerId);
        if (customer == null)
            throw new EntityNotFoundException("User", request.Order.CustomerId);

        var restaurant = await _unitOfWork.Restaurants.GetByIdAsync(request.Order.RestaurantId);
        if (restaurant == null)
            throw new EntityNotFoundException("Restaurant", request.Order.RestaurantId);

        var order = new Order(request.Order.CustomerId, request.Order.RestaurantId);

        foreach (var itemDto in request.Order.Items)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(itemDto.ProductId);
            if (product == null)
                throw new EntityNotFoundException("Product", itemDto.ProductId);

            order.AddItem(itemDto.ProductId, product.Name, product.Price, itemDto.Quantity);
        }

        await _unitOfWork.Orders.AddAsync(order);
        await _unitOfWork.SaveChangesAsync();

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
