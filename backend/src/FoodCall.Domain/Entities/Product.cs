namespace FoodCall.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public bool Active { get; private set; }

        public Product(string name, string description, decimal price, bool active)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            Active = active;
        }

        public void UpdateDetails(string name, string description, decimal price, bool active)
        {
            Name = name;
            Description = description;
            Price = price;
            Active = active;
        }

        public void Deactivate() => Active = false;
    }
    
}