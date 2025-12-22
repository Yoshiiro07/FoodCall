using FoodCall.Domain.Entities;

namespace FoodCall.Domain.Interfaces;

public interface IPaymentRepository : IRepository<Payment>
{
    Task<Payment?> GetByOrderIdAsync(int orderId);
    Task<IEnumerable<Payment>> GetByStatusAsync(PaymentStatus status);
}