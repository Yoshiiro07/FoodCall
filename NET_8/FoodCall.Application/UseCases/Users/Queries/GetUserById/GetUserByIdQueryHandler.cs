using FoodCall.Application.DTOs;
using FoodCall.Domain.Exceptions;
using FoodCall.Domain.Repositories;
using MediatR;

namespace FoodCall.Application.UseCases.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.Id);
        if (user == null)
            throw new EntityNotFoundException("User", request.Id);

        var addressesDtos = user.Addresses.Select(a => new AddressDto(
            a.Street,
            a.Number,
            a.Neighborhood,
            a.City,
            a.ZipCode
        )).ToList();

        return new UserDto(user.Id, user.Name, user.Email, user.Phone, addressesDtos);
    }
}
