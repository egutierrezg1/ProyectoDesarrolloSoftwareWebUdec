using ArquitecturaHexagonalDDD.App.Domain.Shared;

namespace ArquitecturaHexagonalDDD.App.Domain.Contratos.ValueObject;

public class Monto : Shared.ValueObject
{
    public decimal Value { get; }

    public Monto(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("El monto no puede ser negativo", nameof(value));
        Value = decimal.Round(value, 2, MidpointRounding.AwayFromZero);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator decimal(Monto v) => v.Value;
    public static implicit operator Monto(decimal value) => new(value);
}

