using ArquitecturaHexagonalDDD.App.Domain.Shared;
using ArquitecturaHexagonalDDD.App.Domain.Contratos.ValueObject;

namespace ArquitecturaHexagonalDDD.App.Domain.Contratos;

public class Contrato : AggregateRoot
{
    public ContratoId Id { get; private set; }
    public DateTime FechaFirma { get; private set; }
    public DateTime FechaInicio { get; private set; }
    public DateTime? FechaFin { get; private set; }
    public Empresa Empresa { get; private set; }
    public EmpleadoId EmpleadoId { get; private set; }
    public Funciones Funciones { get; private set; }
    public Monto Monto { get; private set; }
    public FrecuenciaPago FrecuenciaPago { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Contrato() { }

    public Contrato(
        ContratoId id,
        DateTime fechaFirma,
        DateTime fechaInicio,
        DateTime? fechaFin,
        Empresa empresa,
        EmpleadoId empleadoId,
        Funciones funciones,
        Monto monto,
        FrecuenciaPago frecuenciaPago)
    {
        if (fechaFin.HasValue && fechaFin.Value < fechaInicio)
            throw new ArgumentException("La fecha de fin no puede ser anterior a la de inicio");

        Id = id;
        FechaFirma = fechaFirma;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
        Empresa = empresa;
        EmpleadoId = empleadoId;
        Funciones = funciones;
        Monto = monto;
        FrecuenciaPago = frecuenciaPago;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    public static Contrato Create(
        DateTime fechaFirma,
        DateTime fechaInicio,
        DateTime? fechaFin,
        Empresa empresa,
        EmpleadoId empleadoId,
        Funciones funciones,
        Monto monto,
        FrecuenciaPago frecuenciaPago)
        => new(ContratoId.New(), fechaFirma, fechaInicio, fechaFin, empresa, empleadoId, funciones, monto, frecuenciaPago);

    public void Update(
        DateTime fechaFirma,
        DateTime fechaInicio,
        DateTime? fechaFin,
        Empresa empresa,
        EmpleadoId empleadoId,
        Funciones funciones,
        Monto monto,
        FrecuenciaPago frecuenciaPago)
    {
        if (fechaFin.HasValue && fechaFin.Value < fechaInicio)
            throw new ArgumentException("La fecha de fin no puede ser anterior a la de inicio");

        FechaFirma = fechaFirma;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
        Empresa = empresa;
        EmpleadoId = empleadoId;
        Funciones = funciones;
        Monto = monto;
        FrecuenciaPago = frecuenciaPago;
        UpdatedAt = DateTime.UtcNow;
    }
}

