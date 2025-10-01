using ArquitecturaHexagonalDDD.App.Domain.Shared;

namespace ArquitecturaHexagonalDDD.App.Domain.Contratos.ValueObject;

public class EmpleadoId : Shared.ValueObject
{
    public Guid Value { get; }

    public EmpleadoId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El Id de empleado no puede ser vacío", nameof(value));
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Guid(EmpleadoId id) => id.Value;
    public static implicit operator EmpleadoId(Guid value) => new(value);
}

