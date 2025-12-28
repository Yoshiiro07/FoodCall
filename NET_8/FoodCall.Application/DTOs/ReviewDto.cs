namespace FoodCall.Application.DTOs;

public record ReviewDto(
    Guid Id,
    Guid OrderId,
    Guid CustomerId,
    string CustomerName,
    int RestaurantRating,
    string? RestaurantComment,
    int? CourierRating,
    string? CourierComment,
    DateTime CreatedAt
);

public record CreateReviewDto(
    Guid OrderId,
    Guid CustomerId,
    int RestaurantRating,
    string? RestaurantComment
);
