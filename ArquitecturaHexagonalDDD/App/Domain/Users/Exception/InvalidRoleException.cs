namespace ArquitecturaHexagonalDDD.App.Domain.Users.Exception;

public class InvalidRoleException : DomainException
{
    public InvalidRoleException(string message) : base(message) { }
}
