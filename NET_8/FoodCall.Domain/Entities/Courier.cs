namespace FoodCall.Domain.Entities;

public class Courier
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string VehiclePlate { get; private set; }
    public bool IsAvailable { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Courier(string name, string vehiclePlate)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.");

        if (string.IsNullOrWhiteSpace(vehiclePlate))
            throw new ArgumentException("Vehicle plate is required.");

        Id = Guid.NewGuid();
        Name = name.Trim();
        VehiclePlate = vehiclePlate.Trim().ToUpperInvariant();
        IsAvailable = true;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public void SetAvailable(bool available)
    {
        IsAvailable = available;
        UpdatedAt = DateTime.UtcNow;
    }
}