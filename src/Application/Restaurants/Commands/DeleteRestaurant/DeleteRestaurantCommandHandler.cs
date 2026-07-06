using Domain.Repositories;
using MediatR;

namespace Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler
    {
        public readonly IMediator _mediator;
        public readonly IRestaurantRepository _restaurantRepository;

        public DeleteRestaurantCommandHandler(IMediator mediator, IRestaurantRepository restaurantRepository)
        {
            _mediator = mediator;
            _restaurantRepository = restaurantRepository;
        }

        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(request.Id, cancellationToken);
            if (restaurant == null)
            {
                throw new KeyNotFoundException($"Restaurante com ID {request.Id} não encontrado.");
            }

            await _restaurantRepository.DeleteAsync(restaurant, cancellationToken);
        }
    }
}
