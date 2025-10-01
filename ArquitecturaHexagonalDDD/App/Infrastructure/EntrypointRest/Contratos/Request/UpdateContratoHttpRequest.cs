using System.ComponentModel.DataAnnotations;

namespace ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Contratos.Request;

public class UpdateContratoHttpRequest
{
    [Required]
    public DateTime FechaFirma { get; set; }

    [Required]
    public DateTime FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    [Required, StringLength(100)]
    public string Empresa { get; set; } = string.Empty;

    [Required]
    public Guid EmpleadoId { get; set; }

    [Required, StringLength(2000)]
    public string Funciones { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public decimal Monto { get; set; }

    [Required, StringLength(20)]
    public string FrecuenciaPago { get; set; } = string.Empty;
}

