namespace FoodCall.Domain.Entities;

public class Cart
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    // FK
    public int UserId { get; set; }
    public int? RestaurantId { get; set; } // Nullable - carrinho vazio não tem restaurante
    
    // Relacionamentos
    public User User { get; set; } = null!;
    public Restaurant? Restaurant { get; set; }
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}