using FoodCall.Domain.Exceptions;

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
        ValidateName(name);
        ValidateDocument(document);

        Id = Guid.NewGuid();
        Name = name;
        Document = document;
        IsActive = true;
    }

    public void AddProduct(string name, string description, decimal price)
    {
        var product = new Product(Id, name, description, price);
        Menu.Add(product);
    }

    public void RemoveProduct(Guid productId)
    {
        var product = Menu.FirstOrDefault(p => p.Id == productId);
        
        if (product == null)
            throw new EntityNotFoundException("Product", productId);

        Menu.Remove(product);
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void UpdateName(string name)
    {
        ValidateName(name);
        Name = name;
    }

    private void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidEntityException("Restaurant", "Nome não pode ser vazio");

        if (name.Length < 3)
            throw new InvalidEntityException("Restaurant", "Nome deve ter no mínimo 3 caracteres");

        if (name.Length > 200)
            throw new InvalidEntityException("Restaurant", "Nome deve ter no máximo 200 caracteres");
    }

    private void ValidateDocument(string document)
    {
        if (string.IsNullOrWhiteSpace(document))
            throw new InvalidEntityException("Restaurant", "CNPJ não pode ser vazio");

        // Remove caracteres não numéricos
        var cnpjNumeros = new string(document.Where(char.IsDigit).ToArray());

        if (cnpjNumeros.Length != 14)
            throw new InvalidEntityException("Restaurant", "CNPJ deve conter 14 dígitos");
    }
}
