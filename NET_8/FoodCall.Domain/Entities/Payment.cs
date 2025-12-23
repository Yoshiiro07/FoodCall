using FoodCall.Domain.Enums;
namespace FoodCall.Domain.Entities;

public class Payment
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; private set; }
    public bool IsPaid { get; set; }
    public DateTime PaidAt { get; set; }
}