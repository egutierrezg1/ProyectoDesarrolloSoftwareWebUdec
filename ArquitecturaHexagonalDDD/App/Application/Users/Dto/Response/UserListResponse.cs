namespace ArquitecturaHexagonalDDD.App.Application.Users.Dto.Response;

public class UserListResponse
{
    public IEnumerable<UserResponse> Users { get; set; } = new List<UserResponse>();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
