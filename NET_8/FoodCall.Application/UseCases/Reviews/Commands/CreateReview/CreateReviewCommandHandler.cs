using FoodCall.Application.DTOs;
using FoodCall.Domain.Entities;
using FoodCall.Domain.Exceptions;
using FoodCall.Domain.Repositories;
using MediatR;

namespace FoodCall.Application.UseCases.Reviews.Commands.CreateReview;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ReviewDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateReviewCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ReviewDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(request.Review.OrderId);
        if (order == null)
            throw new EntityNotFoundException("Order", request.Review.OrderId);

        var customer = await _unitOfWork.Users.GetByIdAsync(request.Review.CustomerId);
        if (customer == null)
            throw new EntityNotFoundException("User", request.Review.CustomerId);

        var review = new Review(
            request.Review.OrderId,
            request.Review.CustomerId,
            request.Review.RestaurantRating,
            request.Review.RestaurantComment
        );

        await _unitOfWork.Reviews.AddAsync(review);
        await _unitOfWork.SaveChangesAsync();

        return new ReviewDto(
            review.Id,
            review.OrderId,
            review.CustomerId,
            customer.Name,
            review.RestaurantRating,
            review.RestaurantComment,
            review.CourierRating,
            review.CourierComment,
            review.CreatedAt
        );
    }
}
