using FoodCall.Application.DTOs;
using FoodCall.Domain.Repositories;
using MediatR;

namespace FoodCall.Application.UseCases.Products.Queries.GetProductsByRestaurant;

public class GetProductsByRestaurantQueryHandler : IRequestHandler<GetProductsByRestaurantQuery, List<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductsByRestaurantQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ProductDto>> Handle(GetProductsByRestaurantQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products.GetByRestaurantIdAsync(request.RestaurantId);

        return products.Select(p => new ProductDto(
            p.Id,
            p.RestaurantId,
            p.Name,
            p.Description,
            p.Price
        )).ToList();
    }
}
