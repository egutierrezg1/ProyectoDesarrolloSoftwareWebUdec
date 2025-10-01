namespace ArquitecturaHexagonalDDD.App.Domain.Users.Exception;

public class UserAlreadyInactiveException : DomainException
{
    public UserAlreadyInactiveException() : base("El usuario ya está inactivo") { }
}
