using MediatR;
using Domain.Repositories;

namespace Application.Restaurants.Commands.UpdateRestaurant;
public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand>
{
    public readonly IRestaurantRepository _restaurantRepository;

    public UpdateRestaurantCommandHandler(IRestaurantRepository restaurantRepository)
    {
        _restaurantRepository = restaurantRepository;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }

    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        Id = request.Id;
        Name = request.Name;
        Address = request.Address;
        PhoneNumber = request.PhoneNumber;

        var restaurant = await _restaurantRepository.GetByIdAsync(request.Id, cancellationToken);

        if (restaurant == null)
        {
            throw new Exception($"Restaurante com ID {request.Id} não foi encontrado para atualização.");
        }

        restaurant.Name = request.Name;
        restaurant.Address = request.Address;
        restaurant.PhoneNumber = request.PhoneNumber;
        
        await _restaurantRepository.UpdateAsync(restaurant, cancellationToken);
    }
}