namespace ArquitecturaHexagonalDDD.App.Application.Users.Dto.Query;

public class ListUsersQuery
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }
    public bool? IsActive { get; set; }
    public string? Role { get; set; }
}
