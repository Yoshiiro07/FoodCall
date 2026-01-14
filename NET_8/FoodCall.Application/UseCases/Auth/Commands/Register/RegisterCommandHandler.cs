using FoodCall.Application.DTOs;
using FoodCall.Domain.Entities;
using FoodCall.Domain.Exceptions;
using FoodCall.Domain.Repositories;
using MediatR;

namespace FoodCall.Application.UseCases.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, UserDto>
{
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Verificar se email já existe
        if (await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
            throw new DuplicateEntityException("User", "Email já cadastrado");

        // Hash da senha usando BCrypt
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        // Criar usuário
        var user = new User(request.Name, request.Email, request.Phone, passwordHash);

        await _userRepository.AddAsync(user, cancellationToken);

        return new UserDto(
            user.Id,
            user.Name,
            user.Email,
            user.Phone,
            new List<AddressDto>()
        );
    }
}
