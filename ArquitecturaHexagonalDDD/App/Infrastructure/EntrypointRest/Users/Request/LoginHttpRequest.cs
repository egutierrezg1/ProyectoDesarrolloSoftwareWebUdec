using System.ComponentModel.DataAnnotations;

namespace ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Users.Request;

public class LoginHttpRequest
{
    [Required(ErrorMessage = "El email es requerido")]
    [EmailAddress(ErrorMessage = "El formato del email no es válido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es requerida")]
    public string Password { get; set; } = string.Empty;
}
