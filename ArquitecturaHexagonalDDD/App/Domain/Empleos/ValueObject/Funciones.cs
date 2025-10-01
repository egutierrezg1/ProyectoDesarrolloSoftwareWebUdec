using ArquitecturaHexagonalDDD.App.Domain.Shared;

namespace ArquitecturaHexagonalDDD.App.Domain.Empleos.ValueObject;

public class Funciones : Shared.ValueObject
{
    public string Value { get; }

    public Funciones(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Las funciones no pueden estar vacías", nameof(value));
        if (value.Length > 2000)
            throw new ArgumentException("Las funciones no pueden exceder 2000 caracteres", nameof(value));
        Value = value.Trim();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(Funciones v) => v.Value;
    public static implicit operator Funciones(string value) => new(value);
}
