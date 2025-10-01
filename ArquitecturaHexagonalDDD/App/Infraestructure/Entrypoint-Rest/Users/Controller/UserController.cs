using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArquitecturaHexagonalDDD.App.Application.Users.Dto.Command;
using ArquitecturaHexagonalDDD.App.Application.Users.Dto.Query;
using ArquitecturaHexagonalDDD.App.Application.Users.Port.In;
using ArquitecturaHexagonalDDD.App.Infraestructure.Entrypoint-Rest.Common-ErrorHandler;
using ArquitecturaHexagonalDDD.App.Infraestructure.Entrypoint-Rest.Users.Request;
using ArquitecturaHexagonalDDD.App.Infraestructure.Entrypoint-Rest.Users.Response;

namespace ArquitecturaHexagonalDDD.App.Infraestructure.Entrypoint-Rest.Users.Controller;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ICreateUserUseCase _createUserUseCase;
    private readonly IUpdateUserUseCase _updateUserUseCase;
    private readonly IDeleteUserUseCase _deleteUserUseCase;
    private readonly IGetUserByIdUseCase _getUserByIdUseCase;
    private readonly IListUsersUseCase _listUsersUseCase;
    private readonly ILoginUseCase _loginUseCase;
    private readonly ILogoutUseCase _logoutUseCase;
    private readonly IChangePasswordUseCase _changePasswordUseCase;
    private readonly IRequestPasswordResetUseCase _requestPasswordResetUseCase;
    private readonly IResetPasswordUseCase _resetPasswordUseCase;
    private readonly IMapper _mapper;

    public UserController(
        ICreateUserUseCase createUserUseCase,
        IUpdateUserUseCase updateUserUseCase,
        IDeleteUserUseCase deleteUserUseCase,
        IGetUserByIdUseCase getUserByIdUseCase,
        IListUsersUseCase listUsersUseCase,
        ILoginUseCase loginUseCase,
        ILogoutUseCase logoutUseCase,
        IChangePasswordUseCase changePasswordUseCase,
        IRequestPasswordResetUseCase requestPasswordResetUseCase,
        IResetPasswordUseCase resetPasswordUseCase,
        IMapper mapper)
    {
        _createUserUseCase = createUserUseCase;
        _updateUserUseCase = updateUserUseCase;
        _deleteUserUseCase = deleteUserUseCase;
        _getUserByIdUseCase = getUserByIdUseCase;
        _listUsersUseCase = listUsersUseCase;
        _loginUseCase = loginUseCase;
        _logoutUseCase = logoutUseCase;
        _changePasswordUseCase = changePasswordUseCase;
        _requestPasswordResetUseCase = requestPasswordResetUseCase;
        _resetPasswordUseCase = resetPasswordUseCase;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserHttpRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = _mapper.Map<CreateUserCommand>(request);
            var result = await _createUserUseCase.ExecuteAsync(command);
            var response = _mapper.Map<UserHttpResponse>(result);

            return CreatedAtAction(nameof(GetUserById), new { id = response.Id }, response);
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException(ex);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        try
        {
            var query = new GetUserByIdQuery { Id = id };
            var result = await _getUserByIdUseCase.ExecuteAsync(query);

            if (result == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }

            var response = _mapper.Map<UserHttpResponse>(result);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException(ex);
        }
    }

    [HttpGet]
    public async Task<IActionResult> ListUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 10, 
        [FromQuery] string? searchTerm = null, [FromQuery] bool? isActive = null, [FromQuery] string? role = null)
    {
        try
        {
            var query = new ListUsersQuery
            {
                Page = page,
                PageSize = pageSize,
                SearchTerm = searchTerm,
                IsActive = isActive,
                Role = role
            };

            var result = await _listUsersUseCase.ExecuteAsync(query);
            var response = _mapper.Map<UserListHttpResponse>(result);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException(ex);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserHttpRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = _mapper.Map<UpdateUserCommand>(request);
            command.Id = id;

            var result = await _updateUserUseCase.ExecuteAsync(command);
            var response = _mapper.Map<UserHttpResponse>(result);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException(ex);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        try
        {
            var command = new DeleteUserCommand { Id = id };
            await _deleteUserUseCase.ExecuteAsync(command);

            return NoContent();
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException(ex);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginHttpRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = _mapper.Map<LoginCommand>(request);
            var result = await _loginUseCase.ExecuteAsync(command);
            var response = _mapper.Map<LoginHttpResponse>(result);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException(ex);
        }
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromHeader(Name = "Authorization")] string? authorization)
    {
        try
        {
            if (string.IsNullOrEmpty(authorization) || !authorization.StartsWith("Bearer "))
            {
                return BadRequest(new { message = "Token de autorización requerido" });
            }

            var token = authorization.Substring("Bearer ".Length).Trim();
            await _logoutUseCase.ExecuteAsync(token);

            return Ok(new { message = "Sesión cerrada exitosamente" });
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException(ex);
        }
    }

    [HttpPost("{id}/change-password")]
    public async Task<IActionResult> ChangePassword(Guid id, [FromBody] ChangePasswordHttpRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = _mapper.Map<ChangePasswordCommand>(request);
            command.Id = id;

            await _changePasswordUseCase.ExecuteAsync(command);

            return Ok(new { message = "Contraseña cambiada exitosamente" });
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException(ex);
        }
    }

    [HttpPost("request-password-reset")]
    public async Task<IActionResult> RequestPasswordReset([FromBody] RequestPasswordResetHttpRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = _mapper.Map<RequestPasswordResetCommand>(request);
            await _requestPasswordResetUseCase.ExecuteAsync(command);

            return Ok(new { message = "Si el email existe, se enviará un enlace para restablecer la contraseña" });
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException(ex);
        }
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _resetPasswordUseCase.ExecuteAsync(command);

            return Ok(new { message = "Contraseña restablecida exitosamente" });
        }
        catch (Exception ex)
        {
            return ApiExceptionHandler.HandleException(ex);
        }
    }
}
