using ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Empleos.Response;

namespace ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Empleos.Response;

public class EmpleoListHttpResponse
{
    public IEnumerable<EmpleoHttpResponse> Empleos { get; set; } = Enumerable.Empty<EmpleoHttpResponse>();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}

