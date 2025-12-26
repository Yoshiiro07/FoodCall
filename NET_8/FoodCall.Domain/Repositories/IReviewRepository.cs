using FoodCall.Domain.Entities;

namespace FoodCall.Domain.Repositories;

public interface IReviewRepository : IRepository<Review>
{
    Task<IReadOnlyList<Review>> GetByOrderAsync(Guid orderId, CancellationToken ct = default);
    Task<IReadOnlyList<Review>> GetByCustomerAsync(Guid customerId, CancellationToken ct = default);
}