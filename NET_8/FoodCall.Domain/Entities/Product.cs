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
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name is required.");

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Product description is required.");

        if (price < 0)
            throw new ArgumentException("Product price cannot be negative.");

        Name = name.Trim();
        Description = description.Trim();
        Price = price;
    }

    public void Update(string name, string description, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name is required.");

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Product description is required.");

        if (price < 0)
            throw new ArgumentException("Product price cannot be negative.");

        Name = name.Trim();
        Description = description.Trim();
        Price = price;
    }
}