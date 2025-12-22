using FoodCall.Domain.Entities;

namespace FoodCall.Domain.Interfaces;

public interface IMenuItemRepository : IRepository<MenuItem>
{
    Task<IEnumerable<MenuItem>> GetByRestaurantIdAsync(int restaurantId);
    Task<IEnumerable<MenuItem>> GetByCategoryIdAsync(int categoryId);
    Task<IEnumerable<MenuItem>> GetAvailableByRestaurantIdAsync(int restaurantId);
    Task<IEnumerable<MenuItem>> SearchAsync(string searchTerm);
}