using FoodCall.Domain.Entities;

namespace FoodCall.Domain.Repositories;

public interface IRestaurantRepository : IRepository<Restaurant>
{
    Task<Restaurant?> GetByDocumentAsync(string document, CancellationToken ct = default);
    Task<IReadOnlyList<Product>> GetMenuAsync(Guid restaurantId, CancellationToken ct = default);
    Task<bool> IsActiveAsync(Guid restaurantId, CancellationToken ct = default);
}