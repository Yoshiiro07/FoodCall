namespace FoodCall.Application.DTOs;

public record LoginRequest(
    string Email,
    string Password
);

public record LoginResponse(
    string Token,
    UserAuthDto User
);

public record UserAuthDto(
    Guid Id,
    string Name,
    string Email
);

public record RegisterRequest(
    string Name,
    string Email,
    string Phone,
    string Password
);
