using FoodCall.Domain.Entities;

namespace FoodCall.Domain.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<IReadOnlyList<Product>> GetByRestaurantAsync(Guid restaurantId, CancellationToken ct = default);
    Task<Product?> GetByNameAsync(Guid restaurantId, string name, CancellationToken ct = default);
}