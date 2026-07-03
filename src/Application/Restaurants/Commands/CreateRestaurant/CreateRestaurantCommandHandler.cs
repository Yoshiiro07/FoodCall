using MediatR;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, Guid>
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public CreateRestaurantCommandHandler(IRestaurantRepository restaurantRepository)
        {
            this._restaurantRepository = restaurantRepository;
        }

        public async Task<Guid> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        { 
            var restaurant = new Restaurant
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                CNPJ = request.CNPJ,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _restaurantRepository.AddAsync(restaurant, cancellationToken);
            
            return restaurant.Id;
        }
    }
}
