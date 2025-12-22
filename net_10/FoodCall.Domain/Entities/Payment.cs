namespace FoodCall.Domain.Entities;

public class Payment
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public string? TransactionId { get; set; } // ID da transação no gateway (Stripe, PagSeguro, etc)
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? PaidAt { get; set; }
    
    // FK
    public int OrderId { get; set; }
    
    // Relacionamentos
    public Order Order { get; set; } = null!;
}

public enum PaymentMethod
{
    CreditCard = 0,
    DebitCard = 1,
    Pix = 2,
    Cash = 3,  // Dinheiro na entrega
    VoucherCard = 4 // Vale refeição
}

public enum PaymentStatus
{
    Pending = 0,
    Processing = 1,
    Approved = 2,
    Rejected = 3,
    Refunded = 4
}