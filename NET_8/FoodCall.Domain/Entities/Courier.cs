namespace FoodCall.Domain.Entities;

public class Courier
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string VehiclePlate { get; set; }
    public bool IsAvailable { get; set; }
}