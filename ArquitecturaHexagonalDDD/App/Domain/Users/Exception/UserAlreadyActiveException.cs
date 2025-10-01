namespace ArquitecturaHexagonalDDD.App.Domain.Users.Exception;

public class UserAlreadyActiveException : DomainException
{
    public UserAlreadyActiveException() : base("El usuario ya está activo") { }
}
