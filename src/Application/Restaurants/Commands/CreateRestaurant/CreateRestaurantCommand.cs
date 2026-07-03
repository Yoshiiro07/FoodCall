using MediatR;

namespace Application.Restaurants.Commands.CreateRestaurant;

public record CreateRestaurantCommand(string Name, string Address, string PhoneNumber, string Email, string CNPJ) : IRequest<Guid>;

