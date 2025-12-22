using FoodCall.Domain.Entities;

namespace FoodCall.Domain.Interfaces;

public interface IRestaurantRepository : IRepository<Restaurant>
{
    Task<IEnumerable<Restaurant>> GetActiveRestaurantsAsync();
    Task<IEnumerable<Restaurant>> GetByOwnerIdAsync(int ownerId);
    Task<Restaurant?> GetWithMenuItemsAsync(int id);
}