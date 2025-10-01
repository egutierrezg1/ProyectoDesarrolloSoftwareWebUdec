namespace ArquitecturaHexagonalDDD.App.Application.Contratos.Dto.Query;

public class ListContratosQuery
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Empresa { get; set; }
    public Guid? EmpleadoId { get; set; }
    public DateTime? Desde { get; set; }
    public DateTime? Hasta { get; set; }
    public string? FrecuenciaPago { get; set; }
    public decimal? MinMonto { get; set; }
    public decimal? MaxMonto { get; set; }
}

