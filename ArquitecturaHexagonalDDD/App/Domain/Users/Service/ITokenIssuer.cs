using ArquitecturaHexagonalDDD.App.Domain.Users.ValueObject;

namespace ArquitecturaHexagonalDDD.App.Domain.Users.Service;

public interface ITokenIssuer
{
    string GenerateToken(UserId userId, UserName userName, Role role);
    bool ValidateToken(string token);
    UserId? GetUserIdFromToken(string token);
}
