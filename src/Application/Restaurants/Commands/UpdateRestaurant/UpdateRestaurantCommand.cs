using MediatR;

namespace Application.Restaurants.Commands.UpdateRestaurant;
public record UpdateRestaurantCommand(Guid Id, string Name, string Address, string PhoneNumber) : IRequest;