using ArquitecturaHexagonalDDD.App.Domain.Shared;

namespace ArquitecturaHexagonalDDD.App.Domain.Empleos.ValueObject;

public class Sueldo : Shared.ValueObject
{
    public decimal Value { get; }

    public Sueldo(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("El sueldo no puede ser negativo", nameof(value));
        Value = decimal.Round(value, 2, MidpointRounding.AwayFromZero);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator decimal(Sueldo v) => v.Value;
    public static implicit operator Sueldo(decimal value) => new(value);
}
