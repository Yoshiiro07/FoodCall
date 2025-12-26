namespace FoodCall.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public List<Address> Addresses { get; private set; } = new();
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public User(string name, string email, string phone)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.");

        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Phone is required.");

        Id = Guid.NewGuid();
        Name = name.Trim();
        Email = email.Trim();
        Phone = phone.Trim();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public void AddAddress(Address address)
    {
        if (address is null)
            throw new ArgumentNullException(nameof(address));
        Addresses.Add(address);
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.");
        Name = name.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.");
        Email = email.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Phone is required.");
        Phone = phone.Trim();
        UpdatedAt = DateTime.UtcNow;
    }
}

// Value Object: Address.cs
public record Address(string Street, string Number, string Neighborhood, string City, string ZipCode);