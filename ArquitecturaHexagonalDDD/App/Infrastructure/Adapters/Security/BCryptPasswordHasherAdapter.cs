using BCrypt.Net;
using ArquitecturaHexagonalDDD.App.Application.Users.Port.Out;

namespace ArquitecturaHexagonalDDD.App.Infrastructure.Adapters.Security;

public class BCryptPasswordHasherAdapter : IPasswordHasherPort
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
