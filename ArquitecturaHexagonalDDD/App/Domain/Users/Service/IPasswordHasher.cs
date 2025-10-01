using ArquitecturaHexagonalDDD.App.Domain.Users.ValueObject;

namespace ArquitecturaHexagonalDDD.App.Domain.Users.Service;

public interface IPasswordHasher
{
    PasswordHash HashPassword(string password);
    bool VerifyPassword(string password, PasswordHash passwordHash);
}
