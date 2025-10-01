namespace ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Contratos.Response;

public class ContratoListHttpResponse
{
    public IEnumerable<ContratoHttpResponse> Contratos { get; set; } = Enumerable.Empty<ContratoHttpResponse>();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}

