namespace ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Contratos.Response;

public class ContratoHttpResponse
{
    public Guid Id { get; set; }
    public DateTime FechaFirma { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public string Empresa { get; set; } = string.Empty;
    public Guid EmpleadoId { get; set; }
    public string Funciones { get; set; } = string.Empty;
    public decimal Monto { get; set; }
    public string FrecuenciaPago { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

