using FoodCall.Application.DTOs;
using FoodCall.Domain.Repositories;
using MediatR;

namespace FoodCall.Application.UseCases.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler : IRequestHandler<GetAllRestaurantsQuery, List<RestaurantDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllRestaurantsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        var restaurants = await _unitOfWork.Restaurants.GetAllAsync();

        return restaurants.Select(r => new RestaurantDto(
            r.Id,
            r.Name,
            r.Document,
            r.IsActive
        )).ToList();
    }
}
