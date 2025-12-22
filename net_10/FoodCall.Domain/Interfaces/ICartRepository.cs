using FoodCall.Domain.Entities;

namespace FoodCall.Domain.Interfaces;

public interface ICartRepository : IRepository<Cart>
{
    Task<Cart?> GetByUserIdAsync(int userId);
    Task<Cart?> GetWithItemsAsync(int userId);
    Task ClearCartAsync(int userId);
}