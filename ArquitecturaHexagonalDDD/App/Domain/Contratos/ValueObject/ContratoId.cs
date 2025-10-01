using ArquitecturaHexagonalDDD.App.Domain.Shared;

namespace ArquitecturaHexagonalDDD.App.Domain.Contratos.ValueObject;

public class ContratoId : Shared.ValueObject
{
    public Guid Value { get; }

    public ContratoId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El Id de contrato no puede ser vacío", nameof(value));
        Value = value;
    }

    public static ContratoId New() => new(Guid.NewGuid());

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Guid(ContratoId id) => id.Value;
    public static implicit operator ContratoId(Guid value) => new(value);
}

