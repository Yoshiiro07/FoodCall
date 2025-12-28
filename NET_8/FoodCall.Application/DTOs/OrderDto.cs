using FoodCall.Domain.Enums;

namespace FoodCall.Application.DTOs;

public record OrderDto(
    Guid Id,
    Guid CustomerId,
    Guid RestaurantId,
    decimal TotalValue,
    OrderStatus Status,
    List<OrderItemDto> Items
);

public record OrderItemDto(
    Guid Id,
    Guid ProductId,
    string ProductName,
    decimal UnitPrice,
    int Quantity,
    decimal SubTotal
);

public record CreateOrderDto(
    Guid CustomerId,
    Guid RestaurantId,
    List<CreateOrderItemDto> Items
);

public record CreateOrderItemDto(
    Guid ProductId,
    int Quantity
);
