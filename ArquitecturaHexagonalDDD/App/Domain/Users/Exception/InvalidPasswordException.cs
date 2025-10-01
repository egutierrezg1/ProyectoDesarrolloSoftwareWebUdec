namespace ArquitecturaHexagonalDDD.App.Domain.Users.Exception;

public class InvalidPasswordException : DomainException
{
    public InvalidPasswordException(string message) : base(message) { }
}
