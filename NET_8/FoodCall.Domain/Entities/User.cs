namespace FoodCall.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public List<Address> Addresses { get; private set; } = new();

    public User(string name, string email, string phone)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Phone = phone;
    }

    public void AddAddress(Address address) => Addresses.Add(address);
}

// Value Object: Address.cs
public record Address(string Street, string Number, string Neighborhood, string City, string ZipCode);