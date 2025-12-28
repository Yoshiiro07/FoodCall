using FoodCall.Application.DTOs;
using FoodCall.Domain.Entities;
using FoodCall.Domain.Exceptions;
using FoodCall.Domain.Repositories;
using FoodCall.Domain.ValueObjects;
using MediatR;

namespace FoodCall.Application.UseCases.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _unitOfWork.Users.ExistsByEmailAsync(request.User.Email);
        if (existingUser)
            throw new DuplicateEntityException($"User with email {request.User.Email} already exists");

        var user = new User(request.User.Name, request.User.Email, request.User.Phone);

        foreach (var addressDto in request.User.Addresses)
        {
            var address = new Address(
                addressDto.Street,
                addressDto.Number,
                null, // Complement
                addressDto.Neighborhood,
                addressDto.City,
                "", // State  
                addressDto.ZipCode
            );
            user.AddAddress(address);
        }

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

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
