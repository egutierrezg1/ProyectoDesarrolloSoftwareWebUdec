using ArquitecturaHexagonalDDD.App.Domain.Shared;

namespace ArquitecturaHexagonalDDD.App.Domain.Empleos.ValueObject;

public class Categoria : Shared.ValueObject
{
    public string Value { get; }

    public Categoria(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("La categoría no puede estar vacía", nameof(value));
        if (value.Length > 50)
            throw new ArgumentException("La categoría no puede exceder 50 caracteres", nameof(value));
        Value = value.Trim();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(Categoria v) => v.Value;
    public static implicit operator Categoria(string value) => new(value);
}
