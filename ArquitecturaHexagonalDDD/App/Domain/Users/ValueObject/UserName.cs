using ArquitecturaHexagonalDDD.App.Domain.Shared;

namespace ArquitecturaHexagonalDDD.App.Domain.Users.ValueObject;

public class UserName : Shared.ValueObject
{
    public string Value { get; }

    public UserName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El nombre de usuario no puede estar vacío", nameof(value));
        
        if (value.Length < 3)
            throw new ArgumentException("El nombre de usuario debe tener al menos 3 caracteres", nameof(value));
        
        if (value.Length > 50)
            throw new ArgumentException("El nombre de usuario no puede tener más de 50 caracteres", nameof(value));

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(UserName userName) => userName.Value;
    public static implicit operator UserName(string value) => new(value);
}
