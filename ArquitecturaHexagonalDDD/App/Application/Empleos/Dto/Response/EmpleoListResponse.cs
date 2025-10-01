namespace ArquitecturaHexagonalDDD.App.Application.Empleos.Dto.Response;

public class EmpleoListResponse
{
    public IEnumerable<EmpleoResponse> Empleos { get; set; } = Enumerable.Empty<EmpleoResponse>();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}

