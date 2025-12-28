namespace FoodCall.Application.DTOs;

public record CourierDto(
    Guid Id,
    string Name,
    string VehiclePlate,
    bool IsAvailable
);

public record CreateCourierDto(
    string Name,
    string VehiclePlate
);
