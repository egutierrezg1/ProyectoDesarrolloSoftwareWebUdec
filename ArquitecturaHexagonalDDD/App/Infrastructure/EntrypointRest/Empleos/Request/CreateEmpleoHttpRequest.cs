using System.ComponentModel.DataAnnotations;

namespace ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Empleos.Request;

public class CreateEmpleoHttpRequest
{
    [Required, StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string Categoria { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string AreaTrabajo { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string Empresa { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string Nivel { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public decimal Sueldo { get; set; }

    [Required, StringLength(2000)]
    public string Funciones { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string CargoJefe { get; set; } = string.Empty;
}

