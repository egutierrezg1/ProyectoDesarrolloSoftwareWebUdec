using Microsoft.AspNetCore.Mvc;
using ArquitecturaHexagonalDDD.App.Domain.Users.Exception;

namespace ArquitecturaHexagonalDDD.App.Infraestructure.Entrypoint-Rest.Common-ErrorHandler;

public class ApiExceptionHandler
{
    public static IActionResult HandleException(Exception exception)
    {
        return exception switch
        {
            DomainException domainEx => new BadRequestObjectResult(new { message = domainEx.Message }),
            InvalidPasswordException => new BadRequestObjectResult(new { message = "Error de contraseña", details = exception.Message }),
            InvalidRoleException => new BadRequestObjectResult(new { message = "Rol inválido", details = exception.Message }),
            InvalidUserNameException => new BadRequestObjectResult(new { message = "Nombre de usuario inválido", details = exception.Message }),
            UserAlreadyActiveException => new BadRequestObjectResult(new { message = exception.Message }),
            UserAlreadyInactiveException => new BadRequestObjectResult(new { message = exception.Message }),
            UserNotFoundException => new NotFoundObjectResult(new { message = exception.Message }),
            EmailAlreadyExistsException => new ConflictObjectResult(new { message = exception.Message }),
            ArgumentException => new BadRequestObjectResult(new { message = exception.Message }),
            NotImplementedException => new NotImplementedObjectResult(new { message = exception.Message }),
            _ => new ObjectResult(new { message = "Error interno del servidor" }) { StatusCode = 500 }
        };
    }
}
