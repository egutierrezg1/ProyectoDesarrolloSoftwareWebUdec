using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArquitecturaHexagonalDDD.App.Infrastructure.Adapters.DatabaseEntityFramework.Model;

[Table("Contratos")]
public class ContratoModel
{
    [Key]
    public Guid Id { get; set; }

    public DateTime FechaFirma { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }

    [Required]
    [StringLength(100)]
    public string Empresa { get; set; } = string.Empty;

    public Guid EmpleadoId { get; set; }

    [Required]
    [StringLength(2000)]
    public string Funciones { get; set; } = string.Empty;

    [Column(TypeName = "numeric(18,2)")]
    public decimal Monto { get; set; }

    [Required]
    [StringLength(20)]
    public string FrecuenciaPago { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

