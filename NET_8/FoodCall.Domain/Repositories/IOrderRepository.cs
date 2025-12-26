using FoodCall.Domain.Entities;
using FoodCall.Domain.Enums;

namespace FoodCall.Domain.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    Task<IReadOnlyList<Order>> GetByCustomerIdAsync(Guid customerId, CancellationToken ct = default);
    Task<IReadOnlyList<Order>> GetByRestaurantIdAsync(Guid restaurantId, CancellationToken ct = default);
    Task<IReadOnlyList<Order>> GetByStatusAsync(OrderStatus status, CancellationToken ct = default);
}