namespace FoodCall.Application.DTOs;

public record UserDto(
    Guid Id,
    string Name,
    string Email,
    string Phone,
    List<AddressDto> Addresses
);

public record CreateUserDto(
    string Name,
    string Email,
    string Phone,
    string Password,
    List<CreateAddressDto> Addresses
);

public record AddressDto(
    string Street,
    string Number,
    string Neighborhood,
    string City,
    string ZipCode
);

public record CreateAddressDto(
    string Street,
    string Number,
    string Neighborhood,
    string City,
    string ZipCode
);
