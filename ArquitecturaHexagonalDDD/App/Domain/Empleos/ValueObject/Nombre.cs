using ArquitecturaHexagonalDDD.App.Domain.Shared;

namespace ArquitecturaHexagonalDDD.App.Domain.Empleos.ValueObject;

public class Nombre : Shared.ValueObject
{
    public string Value { get; }

    public Nombre(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El nombre no puede estar vacío", nameof(value));
        if (value.Length > 100)
            throw new ArgumentException("El nombre no puede exceder 100 caracteres", nameof(value));
        Value = value.Trim();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(Nombre n) => n.Value;
    public static implicit operator Nombre(string value) => new(value);
}
