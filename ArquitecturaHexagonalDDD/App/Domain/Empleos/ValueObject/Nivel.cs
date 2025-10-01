using ArquitecturaHexagonalDDD.App.Domain.Shared;

namespace ArquitecturaHexagonalDDD.App.Domain.Empleos.ValueObject;

public class Nivel : Shared.ValueObject
{
    public string Value { get; }

    public Nivel(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El nivel no puede estar vacío", nameof(value));
        if (value.Length > 50)
            throw new ArgumentException("El nivel no puede exceder 50 caracteres", nameof(value));
        Value = value.Trim();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(Nivel v) => v.Value;
    public static implicit operator Nivel(string value) => new(value);
}
