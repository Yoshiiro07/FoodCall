using FoodCall.Application.DTOs;
using MediatR;

namespace FoodCall.Application.UseCases.Couriers.Queries.GetAvailableCouriers;

public record GetAvailableCouriersQuery : IRequest<List<CourierDto>>;
