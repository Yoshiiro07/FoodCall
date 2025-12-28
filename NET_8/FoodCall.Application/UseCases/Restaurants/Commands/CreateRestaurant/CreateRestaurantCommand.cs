using FoodCall.Application.DTOs;
using MediatR;

namespace FoodCall.Application.UseCases.Restaurants.Commands.CreateRestaurant;

public record CreateRestaurantCommand(CreateRestaurantDto Restaurant) : IRequest<RestaurantDto>;
