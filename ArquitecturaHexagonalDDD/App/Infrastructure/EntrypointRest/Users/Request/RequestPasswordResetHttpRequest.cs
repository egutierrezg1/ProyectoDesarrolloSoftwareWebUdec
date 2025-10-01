using System.ComponentModel.DataAnnotations;

namespace ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Users.Request;

public class RequestPasswordResetHttpRequest
{
    [Required(ErrorMessage = "El email es requerido")]
    [EmailAddress(ErrorMessage = "El formato del email no es válido")]
    public string Email { get; set; } = string.Empty;
}
