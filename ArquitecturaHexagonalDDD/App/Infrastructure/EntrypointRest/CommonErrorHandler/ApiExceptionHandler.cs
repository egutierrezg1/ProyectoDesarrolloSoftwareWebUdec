using Microsoft.AspNetCore.Mvc;
using ArquitecturaHexagonalDDD.App.Domain.Users.Exception;

namespace ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.CommonErrorHandler;

public class ApiExceptionHandler
{
    public static IActionResult HandleException(Exception exception)
    {
        return exception switch
        {
            UserNotFoundException => new NotFoundObjectResult(new { message = exception.Message }),
            EmailAlreadyExistsException => new ConflictObjectResult(new { message = exception.Message }),
            InvalidPasswordException => new BadRequestObjectResult(new { message = "Error de contraseña", details = exception.Message }),
            InvalidRoleException => new BadRequestObjectResult(new { message = "Rol inválido", details = exception.Message }),
            InvalidUserNameException => new BadRequestObjectResult(new { message = "Nombre de usuario inválido", details = exception.Message }),
            UserAlreadyActiveException => new BadRequestObjectResult(new { message = exception.Message }),
            UserAlreadyInactiveException => new BadRequestObjectResult(new { message = exception.Message }),
            DomainException domainEx => new BadRequestObjectResult(new { message = domainEx.Message }),
            ArgumentException => new BadRequestObjectResult(new { message = exception.Message }),
            NotImplementedException => new ObjectResult(new { message = exception.Message }) { StatusCode = 501 },
            _ => new ObjectResult(new { message = "Error interno del servidor" }) { StatusCode = 500 }
        };
    }
}
