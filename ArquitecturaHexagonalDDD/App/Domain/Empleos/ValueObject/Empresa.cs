using ArquitecturaHexagonalDDD.App.Domain.Shared;

namespace ArquitecturaHexagonalDDD.App.Domain.Empleos.ValueObject;

public class Empresa : Shared.ValueObject
{
    public string Value { get; }

    public Empresa(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("La empresa no puede estar vacía", nameof(value));
        if (value.Length > 100)
            throw new ArgumentException("La empresa no puede exceder 100 caracteres", nameof(value));
        Value = value.Trim();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(Empresa v) => v.Value;
    public static implicit operator Empresa(string value) => new(value);
}
