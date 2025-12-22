namespace FoodCall.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty; // Ex: #12345
    public decimal SubTotal { get; set; } // Soma dos itens
    public decimal DeliveryFee { get; set; }
    public decimal TotalAmount { get; set; } // SubTotal + DeliveryFee
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeliveredAt { get; set; }
    public string? Notes { get; set; } // Observações do cliente
    
    // FKs
    public int UserId { get; set; }
    public int RestaurantId { get; set; }
    public int AddressId { get; set; }
    public int? DeliveryPersonId { get; set; } // Opcional - pode não ter ainda
    
    // Relacionamentos
    public User User { get; set; } = null!;
    public Restaurant Restaurant { get; set; } = null!;
    public Address DeliveryAddress { get; set; } = null!;
    public DeliveryPerson? DeliveryPerson { get; set; }
    public Payment? Payment { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}

public enum OrderStatus
{
    Pending = 0,        // Aguardando confirmação
    Confirmed = 1,      // Confirmado pelo restaurante
    Preparing = 2,      // Em preparação
    ReadyForDelivery = 3, // Pronto para entrega
    OutForDelivery = 4,   // Saiu para entrega
    Delivered = 5,        // Entregue
    Cancelled = 6         // Cancelado
}