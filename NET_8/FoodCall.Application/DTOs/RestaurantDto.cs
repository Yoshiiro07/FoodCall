namespace FoodCall.Application.DTOs;

public record RestaurantDto(
    Guid Id,
    string Name,
    string Document,
    bool IsActive
);

public record CreateRestaurantDto(
    string Name,
    string Document
);
