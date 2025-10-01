namespace ArquitecturaHexagonalDDD.App.Domain.Users.Exception;

public class UserNotFoundException : DomainException
{
    public UserNotFoundException(Guid userId) : base($"Usuario con ID {userId} no encontrado") { }
}
