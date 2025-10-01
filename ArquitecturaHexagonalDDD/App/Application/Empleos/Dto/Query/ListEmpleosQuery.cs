namespace ArquitecturaHexagonalDDD.App.Application.Empleos.Dto.Query;

public class ListEmpleosQuery
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }
    public string? Categoria { get; set; }
    public string? Empresa { get; set; }
    public string? Nivel { get; set; }
    public string? AreaTrabajo { get; set; }
    public decimal? MinSueldo { get; set; }
    public decimal? MaxSueldo { get; set; }
}

