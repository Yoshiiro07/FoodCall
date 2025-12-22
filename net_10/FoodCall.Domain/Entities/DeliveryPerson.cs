namespace FoodCall.Domain.Entities;

public class DeliveryPerson
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string VehicleType { get; set; } = string.Empty; // Moto, Carro, Bicicleta
    public string VehiclePlate { get; set; } = string.Empty;
    public bool IsAvailable { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Relacionamentos
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}