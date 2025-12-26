using FoodCall.Domain.Enums;

namespace FoodCall.Domain.Entities;

public class Payment
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public decimal Amount { get; private set; }
    public PaymentMethod Method { get; private set; }
    public bool IsPaid { get; private set; }
    public DateTime? PaidAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Payment(Guid orderId, decimal amount, PaymentMethod method)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive.");

        Id = Guid.NewGuid();
        OrderId = orderId;
        Amount = amount;
        Method = method;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public void MarkPaid()
    {
        if (IsPaid)
            throw new InvalidOperationException("Payment already marked as paid.");

        IsPaid = true;
        PaidAt = DateTime.UtcNow;
        UpdatedAt = PaidAt.Value;
    }

    public void UpdateAmount(decimal amount)
    {
        if (IsPaid)
            throw new InvalidOperationException("Cannot change amount after payment.");

        if (amount <= 0)
            throw new ArgumentException("Amount must be positive.");

        Amount = amount;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeMethod(PaymentMethod method)
    {
        if (IsPaid)
            throw new InvalidOperationException("Cannot change method after payment.");

        Method = method;
        UpdatedAt = DateTime.UtcNow;
    }
}