namespace FoodCall.Domain.Entities;

public class Restaurant
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Document { get; private set; } // CNPJ
    public bool IsActive { get; private set; }
    public List<Product> Menu { get; private set; } = new();

    public Restaurant(string name, string document)
    {
        Id = Guid.NewGuid();
        Name = name;
        Document = document;
        IsActive = true;
    }

    public void AddProduct(string name, string description, decimal price)
    {
        Menu.Add(new Product(Id, name, description, price));
    }
}