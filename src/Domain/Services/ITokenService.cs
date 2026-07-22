using Domain.Entities;

namespace Domain.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}