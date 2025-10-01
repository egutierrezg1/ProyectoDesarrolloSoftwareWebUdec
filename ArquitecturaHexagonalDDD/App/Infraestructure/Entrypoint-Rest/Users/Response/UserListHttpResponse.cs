namespace ArquitecturaHexagonalDDD.App.Infraestructure.Entrypoint-Rest.Users.Response;

public class UserListHttpResponse
{
    public IEnumerable<UserHttpResponse> Users { get; set; } = new List<UserHttpResponse>();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
