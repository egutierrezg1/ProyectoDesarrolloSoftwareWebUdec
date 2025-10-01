using System.ComponentModel.DataAnnotations;

namespace ArquitecturaHexagonalDDD.App.Application.Users.Dto.Command;

public class LoginCommand
{
    [Required(ErrorMessage = "El email es requerido")]
    [EmailAddress(ErrorMessage = "El formato del email no es válido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es requerida")]
    public string Password { get; set; } = string.Empty;
}
