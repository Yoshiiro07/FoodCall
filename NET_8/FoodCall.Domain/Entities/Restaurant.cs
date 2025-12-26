namespace FoodCall.Domain.Entities;

public class Restaurant
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Document { get; private set; } // CNPJ
    public bool IsActive { get; private set; }
    public List<Product> Menu { get; private set; } = new();
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Restaurant(string name, string document)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.");

        if (string.IsNullOrWhiteSpace(document))
            throw new ArgumentException("Document (CNPJ) is required.");

        Id = Guid.NewGuid();
        Name = name.Trim();
        Document = document.Trim();
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.");
        Name = name.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateDocument(string document)
    {
        if (string.IsNullOrWhiteSpace(document))
            throw new ArgumentException("Document (CNPJ) is required.");
        Document = document.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    public Product AddProduct(string name, string description, decimal price)
    {
        if (!IsActive)
            throw new InvalidOperationException("Cannot add products to an inactive restaurant.");
        if (Menu.Any(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Product name already exists in the menu.");

        var product = new Product(Id, name, description, price);
        Menu.Add(product);
        UpdatedAt = DateTime.UtcNow;
        return product;
    }

    public bool RemoveProduct(Guid productId)
    {
        var removed = Menu.RemoveAll(p => p.Id == productId);
        if (removed > 0)
            UpdatedAt = DateTime.UtcNow;
        return removed > 0;
    }

    public void UpdateProduct(Guid productId, string name, string description, decimal price)
    {
        var product = Menu.FirstOrDefault(p => p.Id == productId);
        if (product is null)
            throw new InvalidOperationException("Product not found in the menu.");

        if (Menu.Any(p => p.Id != productId && string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Another product with the same name already exists.");

        product.Update(name, description, price);
        UpdatedAt = DateTime.UtcNow;
    }
}