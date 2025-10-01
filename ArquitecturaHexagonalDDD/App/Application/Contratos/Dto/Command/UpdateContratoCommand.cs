namespace ArquitecturaHexagonalDDD.App.Application.Contratos.Dto.Command;

public class UpdateContratoCommand
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
}

