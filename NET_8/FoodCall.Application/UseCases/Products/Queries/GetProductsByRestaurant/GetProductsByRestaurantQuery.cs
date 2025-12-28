using FoodCall.Application.DTOs;
using MediatR;

namespace FoodCall.Application.UseCases.Products.Queries.GetProductsByRestaurant;

public record GetProductsByRestaurantQuery(Guid RestaurantId) : IRequest<List<ProductDto>>;
