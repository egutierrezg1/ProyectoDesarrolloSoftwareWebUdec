namespace ArquitecturaHexagonalDDD.App.Domain.Users.Exception;

public class InvalidUserNameException : DomainException
{
    public InvalidUserNameException(string message) : base(message) { }
}
