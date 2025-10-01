using AutoMapper;
using ArquitecturaHexagonalDDD.App.Application.Users.Dto.Query;
using ArquitecturaHexagonalDDD.App.Application.Users.Dto.Response;
using ArquitecturaHexagonalDDD.App.Application.Users.Port.In;
using ArquitecturaHexagonalDDD.App.Domain.Users.Repository;

namespace ArquitecturaHexagonalDDD.App.Application.Users.Service;

public class ListUsersService : IListUsersUseCase
{
    private readonly IUsuarioRepository _userRepository;
    private readonly IMapper _mapper;

    public ListUsersService(IUsuarioRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserListResponse> ExecuteAsync(ListUsersQuery query)
    {
        var users = await _userRepository.GetAllAsync();
        
        // Aplicar filtros
        if (query.IsActive.HasValue)
        {
            users = users.Where(u => u.IsActive == query.IsActive.Value);
        }

        if (!string.IsNullOrEmpty(query.Role))
        {
            users = users.Where(u => u.Role.Value == query.Role);
        }

        if (!string.IsNullOrEmpty(query.SearchTerm))
        {
            users = users.Where(u => 
                u.UserName.Value.Contains(query.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                u.Email.Value.Contains(query.SearchTerm, StringComparison.OrdinalIgnoreCase));
        }

        var totalCount = users.Count();
        
        // Aplicar paginación
        var pagedUsers = users
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize);

        return new UserListResponse
        {
            Users = _mapper.Map<IEnumerable<UserResponse>>(pagedUsers),
            TotalCount = totalCount,
            Page = query.Page,
            PageSize = query.PageSize
        };
    }
}
