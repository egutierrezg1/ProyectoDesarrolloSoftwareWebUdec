using ArquitecturaHexagonalDDD.App.Domain.Shared;

namespace ArquitecturaHexagonalDDD.App.Domain.Users.ValueObject;

public class PasswordHash : Shared.ValueObject
{
    public string Value { get; }

    public PasswordHash(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El hash de la contraseña no puede estar vacío", nameof(value));

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(PasswordHash passwordHash) => passwordHash.Value;
    public static implicit operator PasswordHash(string value) => new(value);
}
