namespace ArquitecturaHexagonalDDD.App.Domain.Users.Exception;

public class EmailAlreadyExistsException : DomainException
{
    public EmailAlreadyExistsException(string email) : base($"El email {email} ya está registrado") { }
}
