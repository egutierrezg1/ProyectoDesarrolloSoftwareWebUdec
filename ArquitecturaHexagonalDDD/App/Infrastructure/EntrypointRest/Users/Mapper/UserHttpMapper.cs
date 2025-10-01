using AutoMapper;
using ArquitecturaHexagonalDDD.App.Application.Users.Dto.Command;
using ArquitecturaHexagonalDDD.App.Application.Users.Dto.Query;
using ArquitecturaHexagonalDDD.App.Application.Users.Dto.Response;
using ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Users.Request;
using ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Users.Response;

namespace ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Users.Mapper;

public class UserHttpMapper : Profile
{
    public UserHttpMapper()
    {
        // Request to Command
        CreateMap<CreateUserHttpRequest, CreateUserCommand>();
        CreateMap<UpdateUserHttpRequest, UpdateUserCommand>();
        CreateMap<LoginHttpRequest, LoginCommand>();
        CreateMap<ChangePasswordHttpRequest, ChangePasswordCommand>();
        CreateMap<RequestPasswordResetHttpRequest, RequestPasswordResetCommand>();

        // Response to HttpResponse
        CreateMap<UserResponse, UserHttpResponse>();
        CreateMap<UserListResponse, UserListHttpResponse>();
        CreateMap<LoginResponse, LoginHttpResponse>();
    }
}
