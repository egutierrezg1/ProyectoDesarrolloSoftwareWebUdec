namespace ArquitecturaHexagonalDDD.App.Domain.Users.Service;

public interface IPasswordStrengthEvaluator
{
    bool IsStrongPassword(string password);
    string GetPasswordStrengthMessage(string password);
}
