using FoodCall.Application.DTOs;
using MediatR;

namespace FoodCall.Application.UseCases.Reviews.Queries.GetReviewsByRestaurant;

public record GetReviewsByRestaurantQuery(Guid RestaurantId) : IRequest<List<ReviewDto>>;
