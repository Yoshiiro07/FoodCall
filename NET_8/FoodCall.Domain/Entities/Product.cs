namespace FoodCall.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public Guid RestaurantId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; } // Agora garantimos o valor no construtor
    public decimal Price { get; private set; }

    // Construtor com 4 argumentos que o Restaurant espera
    public Product(Guid restaurantId, string name, string description, decimal price)
    {
        Id = Guid.NewGuid();
        RestaurantId = restaurantId;
        Name = name;
        Description = description;
        Price = price;
    }
}