using MediatR;

namespace Application.Restaurants.Commands.DeleteRestaurant
{
    public record DeleteRestaurantCommand(Guid Id) : IRequest;
}
