using ArquitecturaHexagonalDDD.App.Domain.Shared;

namespace ArquitecturaHexagonalDDD.App.Domain.Empleos.ValueObject;

public class AreaTrabajo : Shared.ValueObject
{
    public string Value { get; }

    public AreaTrabajo(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El área de trabajo no puede estar vacía", nameof(value));
        if (value.Length > 100)
            throw new ArgumentException("El área de trabajo no puede exceder 100 caracteres", nameof(value));
        Value = value.Trim();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(AreaTrabajo v) => v.Value;
    public static implicit operator AreaTrabajo(string value) => new(value);
}
