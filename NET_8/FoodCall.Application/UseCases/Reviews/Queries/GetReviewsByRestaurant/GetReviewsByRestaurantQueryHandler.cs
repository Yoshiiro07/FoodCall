using FoodCall.Application.DTOs;
using FoodCall.Domain.Repositories;
using MediatR;

namespace FoodCall.Application.UseCases.Reviews.Queries.GetReviewsByRestaurant;

public class GetReviewsByRestaurantQueryHandler : IRequestHandler<GetReviewsByRestaurantQuery, List<ReviewDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetReviewsByRestaurantQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ReviewDto>> Handle(GetReviewsByRestaurantQuery request, CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.Orders.GetByRestaurantIdAsync(request.RestaurantId);
        var orderIds = orders.Select(o => o.Id).ToList();

        var allReviews = await _unitOfWork.Reviews.GetAllAsync();
        var restaurantReviews = allReviews.Where(r => orderIds.Contains(r.OrderId)).ToList();

        var reviewDtos = new List<ReviewDto>();
        foreach (var review in restaurantReviews)
        {
            var customer = await _unitOfWork.Users.GetByIdAsync(review.CustomerId);
            reviewDtos.Add(new ReviewDto(
                review.Id,
                review.OrderId,
                review.CustomerId,
                customer?.Name ?? "Unknown",
                review.RestaurantRating,
                review.RestaurantComment,
                review.CourierRating,
                review.CourierComment,
                review.CreatedAt
            ));
        }

        return reviewDtos;
    }
}
