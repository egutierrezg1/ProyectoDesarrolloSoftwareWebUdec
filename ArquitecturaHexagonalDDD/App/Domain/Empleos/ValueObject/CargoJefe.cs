using ArquitecturaHexagonalDDD.App.Domain.Shared;

namespace ArquitecturaHexagonalDDD.App.Domain.Empleos.ValueObject;

public class CargoJefe : Shared.ValueObject
{
    public string Value { get; }

    public CargoJefe(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El cargo del jefe no puede estar vacío", nameof(value));
        if (value.Length > 100)
            throw new ArgumentException("El cargo del jefe no puede exceder 100 caracteres", nameof(value));
        Value = value.Trim();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(CargoJefe v) => v.Value;
    public static implicit operator CargoJefe(string value) => new(value);
}
