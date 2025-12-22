using FoodCall.Domain.Entities;

namespace FoodCall.Domain.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetByUserIdAsync(int userId);
    Task<IEnumerable<Order>> GetByRestaurantIdAsync(int restaurantId);
    Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status);
    Task<Order?> GetWithDetailsAsync(int id); // Order com Items, Payment, etc
    Task<string> GenerateOrderNumberAsync();
}