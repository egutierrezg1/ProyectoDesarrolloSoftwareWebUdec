using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArquitecturaHexagonalDDD.App.Infrastructure.Adapters.DatabaseEntityFramework.Model;

[Table("Empleos")]
public class EmpleoModel
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Categoria { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string AreaTrabajo { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Empresa { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Nivel { get; set; } = string.Empty;

    [Column(TypeName = "numeric(18,2)")]
    public decimal Sueldo { get; set; }

    [Required]
    [StringLength(2000)]
    public string Funciones { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string CargoJefe { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

