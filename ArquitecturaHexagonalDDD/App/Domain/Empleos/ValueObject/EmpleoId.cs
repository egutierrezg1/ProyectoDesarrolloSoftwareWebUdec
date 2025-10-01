using ArquitecturaHexagonalDDD.App.Domain.Shared;

namespace ArquitecturaHexagonalDDD.App.Domain.Empleos.ValueObject;

public class EmpleoId : Shared.ValueObject
{
    public Guid Value { get; }

    public EmpleoId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El Id de empleo no puede ser vacío", nameof(value));
        Value = value;
    }

    public static EmpleoId New() => new(Guid.NewGuid());

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Guid(EmpleoId id) => id.Value;
    public static implicit operator EmpleoId(Guid value) => new(value);
}
