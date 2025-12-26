using FoodCall.Domain.Entities;

namespace FoodCall.Domain.Repositories;

public interface IPaymentRepository : IRepository<Payment>
{
    Task<Payment?> GetByOrderAsync(Guid orderId, CancellationToken ct = default);
}