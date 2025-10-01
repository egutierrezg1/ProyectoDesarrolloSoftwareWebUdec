using ArquitecturaHexagonalDDD.App.Domain.Users.ValueObject;

namespace ArquitecturaHexagonalDDD.App.Application.Users.Port.Out;

public interface IPasswordHasherPort
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}

public interface IPasswordStrengthPolicyPort
{
    bool IsStrongPassword(string password);
    string GetPasswordStrengthMessage(string password);
}

public interface ITokenIssuerPort
{
    string GenerateToken(Guid userId, string userName, string role);
    bool ValidateToken(string token);
    Guid? GetUserIdFromToken(string token);
}

public interface IUnitOfWorkPort
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    Task SaveChangesAsync();
}
