namespace FoodCall.Domain.Entities;

public class CartItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public string? SpecialInstructions { get; set; }
    
    // FKs
    public int CartId { get; set; }
    public int MenuItemId { get; set; }
    
    // Relacionamentos
    public Cart Cart { get; set; } = null!;
    public MenuItem MenuItem { get; set; } = null!;
}