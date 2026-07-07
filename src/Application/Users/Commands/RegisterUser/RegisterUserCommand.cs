using MediatR;

namespace Application.Users.Commands.RegisterUser;

public record RegisterUserCommand(string Name, string Email, string Password, string PhoneNumber, string Address) : IRequest<Guid>;