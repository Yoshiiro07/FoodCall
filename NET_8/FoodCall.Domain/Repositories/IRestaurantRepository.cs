using FoodCall.Domain.Entities;

namespace FoodCall.Domain.Repositories;

public interface IRestaurantRepository
{
    Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Restaurant?> GetByDocumentAsync(string document, CancellationToken cancellationToken = default);
    Task<IEnumerable<Restaurant>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Restaurant>> GetActiveRestaurantsAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Restaurant restaurant, CancellationToken cancellationToken = default);
    Task UpdateAsync(Restaurant restaurant, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByDocumentAsync(string document, CancellationToken cancellationToken = default);
}
