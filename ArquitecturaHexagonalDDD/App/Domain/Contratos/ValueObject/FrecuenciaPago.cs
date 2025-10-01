using ArquitecturaHexagonalDDD.App.Domain.Shared;

namespace ArquitecturaHexagonalDDD.App.Domain.Contratos.ValueObject;

public class FrecuenciaPago : Shared.ValueObject
{
    public string Value { get; }

    private static readonly HashSet<string> Allowed = new(StringComparer.OrdinalIgnoreCase)
    {
        "Mensual", "Quincenal", "Semanal", "Bimestral", "Trimestral",
    };

    public FrecuenciaPago(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("La frecuencia de pago no puede estar vacía", nameof(value));
        if (!Allowed.Contains(value))
            throw new ArgumentException("Frecuencia de pago no válida", nameof(value));
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }

    public static implicit operator string(FrecuenciaPago v) => v.Value;
    public static implicit operator FrecuenciaPago(string value) => new(value);
}

