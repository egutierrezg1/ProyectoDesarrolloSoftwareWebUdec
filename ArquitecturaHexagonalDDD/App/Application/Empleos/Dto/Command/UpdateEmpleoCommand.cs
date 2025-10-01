namespace ArquitecturaHexagonalDDD.App.Application.Empleos.Dto.Command;

public class UpdateEmpleoCommand
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public string AreaTrabajo { get; set; } = string.Empty;
    public string Empresa { get; set; } = string.Empty;
    public string Nivel { get; set; } = string.Empty;
    public decimal Sueldo { get; set; }
    public string Funciones { get; set; } = string.Empty;
    public string CargoJefe { get; set; } = string.Empty;
}

