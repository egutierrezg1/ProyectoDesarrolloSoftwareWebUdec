using ArquitecturaHexagonalDDD.App.Application.Users.Dto.Command;
using ArquitecturaHexagonalDDD.App.Application.Users.Dto.Query;
using ArquitecturaHexagonalDDD.App.Application.Users.Dto.Response;

namespace ArquitecturaHexagonalDDD.App.Application.Users.Port.In;

public interface ICreateUserUseCase
{
    Task<UserResponse> ExecuteAsync(CreateUserCommand command);
}

public interface IUpdateUserUseCase
{
    Task<UserResponse> ExecuteAsync(UpdateUserCommand command);
}

public interface IDeleteUserUseCase
{
    Task ExecuteAsync(DeleteUserCommand command);
}

public interface IGetUserByIdUseCase
{
    Task<UserResponse?> ExecuteAsync(GetUserByIdQuery query);
}

public interface IListUsersUseCase
{
    Task<UserListResponse> ExecuteAsync(ListUsersQuery query);
}

public interface ILoginUseCase
{
    Task<LoginResponse> ExecuteAsync(LoginCommand command);
}

public interface ILogoutUseCase
{
    Task ExecuteAsync(string token);
}

public interface IChangePasswordUseCase
{
    Task ExecuteAsync(ChangePasswordCommand command);
}

public interface IRequestPasswordResetUseCase
{
    Task ExecuteAsync(RequestPasswordResetCommand command);
}

public interface IResetPasswordUseCase
{
    Task ExecuteAsync(ResetPasswordCommand command);
}
