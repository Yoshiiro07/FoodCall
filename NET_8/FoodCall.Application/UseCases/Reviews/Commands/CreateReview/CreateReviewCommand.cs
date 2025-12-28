using FoodCall.Application.DTOs;
using MediatR;

namespace FoodCall.Application.UseCases.Reviews.Commands.CreateReview;

public record CreateReviewCommand(CreateReviewDto Review) : IRequest<ReviewDto>;
