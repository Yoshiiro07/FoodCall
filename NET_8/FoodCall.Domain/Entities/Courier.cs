using FoodCall.Domain.Exceptions;

namespace FoodCall.Domain.Entities;

public class Courier
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string VehiclePlate { get; private set; }
    public bool IsAvailable { get; private set; }

    public Courier(string name, string vehiclePlate)
    {
        ValidateName(name);
        ValidateVehiclePlate(vehiclePlate);

        Id = Guid.NewGuid();
        Name = name;
        VehiclePlate = vehiclePlate;
        IsAvailable = true;
    }

    public void MarkAsAvailable()
    {
        IsAvailable = true;
    }

    public void MarkAsUnavailable()
    {
        IsAvailable = false;
    }

    public void UpdateName(string name)
    {
        ValidateName(name);
        Name = name;
    }

    public void UpdateVehiclePlate(string vehiclePlate)
    {
        ValidateVehiclePlate(vehiclePlate);
        VehiclePlate = vehiclePlate;
    }

    private void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidEntityException("Courier", "Nome não pode ser vazio");

        if (name.Length < 3)
            throw new InvalidEntityException("Courier", "Nome deve ter no mínimo 3 caracteres");

        if (name.Length > 200)
            throw new InvalidEntityException("Courier", "Nome deve ter no máximo 200 caracteres");
    }

    private void ValidateVehiclePlate(string vehiclePlate)
    {
        if (string.IsNullOrWhiteSpace(vehiclePlate))
            throw new InvalidEntityException("Courier", "Placa do veículo não pode ser vazia");

        // Remove caracteres não alfanuméricos
        var plateClean = new string(vehiclePlate.Where(char.IsLetterOrDigit).ToArray());

        if (plateClean.Length != 7)
            throw new InvalidEntityException("Courier", "Placa do veículo deve ter 7 caracteres");
    }
}
