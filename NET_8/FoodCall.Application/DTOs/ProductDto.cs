namespace FoodCall.Application.DTOs;

public record ProductDto(
    Guid Id,
    Guid RestaurantId,
    string Name,
    string Description,
    decimal Price
);

public record CreateProductDto(
    Guid RestaurantId,
    string Name,
    string Description,
    decimal Price
);
