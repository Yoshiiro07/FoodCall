namespace FoodCall.Domain.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; } // Preço no momento da compra
    public decimal TotalPrice { get; set; } // Quantity * UnitPrice
    public string? SpecialInstructions { get; set; } // "Sem cebola", "Bem passado", etc
    
    // FKs
    public int OrderId { get; set; }
    public int MenuItemId { get; set; }
    
    // Relacionamentos
    public Order Order { get; set; } = null!;
    public MenuItem MenuItem { get; set; } = null!;
}