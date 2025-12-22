using System.Reflection.Metadata.Ecma335;

namespace  FoodCall.Domain.Entities;

public class MenuItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsAvailable { get; set; } = true;
    public DateTime CreateAt {get;set;} = DateTime.UtcNow;

    // Estrangeiras
    public int RestaurantId { get; set; }
    public int CategoryId { get; set; }

    // Relações
    public Restaurant Restaurant { get; set; } = null!;
    public Category Category { get; set; } = null!;

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}