using FoodCall.Application.DTOs;
using FoodCall.Domain.Entities;
using FoodCall.Domain.Repositories;
using MediatR;

namespace FoodCall.Application.UseCases.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, RestaurantDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateRestaurantCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<RestaurantDto> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = new Restaurant(request.Restaurant.Name, request.Restaurant.Document);

        await _unitOfWork.Restaurants.AddAsync(restaurant);
        await _unitOfWork.SaveChangesAsync();

        return new RestaurantDto(restaurant.Id, restaurant.Name, restaurant.Document, restaurant.IsActive);
    }
}
