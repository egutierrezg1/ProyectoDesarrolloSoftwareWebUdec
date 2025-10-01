namespace ArquitecturaHexagonalDDD.App.Application.Contratos.Dto.Response;

public class ContratoListResponse
{
    public IEnumerable<ContratoResponse> Contratos { get; set; } = Enumerable.Empty<ContratoResponse>();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}

