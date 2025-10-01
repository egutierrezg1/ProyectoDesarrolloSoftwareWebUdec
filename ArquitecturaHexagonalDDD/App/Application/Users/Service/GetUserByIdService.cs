using AutoMapper;
using ArquitecturaHexagonalDDD.App.Application.Users.Dto.Query;
using ArquitecturaHexagonalDDD.App.Application.Users.Dto.Response;
using ArquitecturaHexagonalDDD.App.Application.Users.Port.In;
using ArquitecturaHexagonalDDD.App.Domain.Users.Repository;
using ArquitecturaHexagonalDDD.App.Domain.Users.ValueObject;
using ArquitecturaHexagonalDDD.App.Domain.Users.Exception;

namespace ArquitecturaHexagonalDDD.App.Application.Users.Service;

public class GetUserByIdService : IGetUserByIdUseCase
{
    private readonly IUsuarioRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdService(IUsuarioRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserResponse?> ExecuteAsync(GetUserByIdQuery query)
    {
        var userId = new UserId(query.Id);
        var user = await _userRepository.GetByIdAsync(userId);

        return user == null ? null : _mapper.Map<UserResponse>(user);
    }
}
