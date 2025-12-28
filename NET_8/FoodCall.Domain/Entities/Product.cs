using FoodCall.Domain.Exceptions;

namespace FoodCall.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public Guid RestaurantId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }

    public Product(Guid restaurantId, string name, string description, decimal price)
    {
        ValidateRestaurantId(restaurantId);
        ValidateName(name);
        ValidateDescription(description);
        ValidatePrice(price);

        Id = Guid.NewGuid();
        RestaurantId = restaurantId;
        Name = name;
        Description = description;
        Price = price;
    }

    public void UpdatePrice(decimal price)
    {
        ValidatePrice(price);
        Price = price;
    }

    public void UpdateName(string name)
    {
        ValidateName(name);
        Name = name;
    }

    public void UpdateDescription(string description)
    {
        ValidateDescription(description);
        Description = description;
    }

    private void ValidateRestaurantId(Guid restaurantId)
    {
        if (restaurantId == Guid.Empty)
            throw new InvalidEntityException("Product", "RestaurantId não pode ser vazio");
    }

    private void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidEntityException("Product", "Nome não pode ser vazio");

        if (name.Length < 3)
            throw new InvalidEntityException("Product", "Nome deve ter no mínimo 3 caracteres");

        if (name.Length > 200)
            throw new InvalidEntityException("Product", "Nome deve ter no máximo 200 caracteres");
    }

    private void ValidateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new InvalidEntityException("Product", "Descrição não pode ser vazia");

        if (description.Length > 500)
            throw new InvalidEntityException("Product", "Descrição deve ter no máximo 500 caracteres");
    }

    private void ValidatePrice(decimal price)
    {
        if (price <= 0)
            throw new InvalidEntityException("Product", "Preço deve ser maior que zero");

        if (price > 999999.99m)
            throw new InvalidEntityException("Product", "Preço deve ser menor que R$ 999.999,99");
    }
}
