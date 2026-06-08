using Domain.Entities;

namespace Domain.Repositories;

public interface IRestaurantRepository
{
    Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Restaurant>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Restaurant restaurant, CancellationToken cancellationToken);
    Task UpdateAsync(Restaurant restaurant, CancellationToken cancellationToken);
    Task DeleteAsync(Restaurant restaurant, CancellationToken cancellationToken);
}