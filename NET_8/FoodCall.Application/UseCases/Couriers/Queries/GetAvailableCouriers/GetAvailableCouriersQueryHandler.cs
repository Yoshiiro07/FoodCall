using FoodCall.Application.DTOs;
using FoodCall.Domain.Repositories;
using MediatR;

namespace FoodCall.Application.UseCases.Couriers.Queries.GetAvailableCouriers;

public class GetAvailableCouriersQueryHandler : IRequestHandler<GetAvailableCouriersQuery, List<CourierDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAvailableCouriersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<CourierDto>> Handle(GetAvailableCouriersQuery request, CancellationToken cancellationToken)
    {
        var couriers = await _unitOfWork.Couriers.GetAvailableCouriersAsync();

        return couriers.Select(c => new CourierDto(
            c.Id,
            c.Name,
            c.VehiclePlate,
            c.IsAvailable
        )).ToList();
    }
}
