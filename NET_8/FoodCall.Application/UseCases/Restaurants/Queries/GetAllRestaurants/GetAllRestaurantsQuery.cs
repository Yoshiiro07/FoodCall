using FoodCall.Application.DTOs;
using MediatR;

namespace FoodCall.Application.UseCases.Restaurants.Queries.GetAllRestaurants;

public record GetAllRestaurantsQuery : IRequest<List<RestaurantDto>>;
