using System.ComponentModel.DataAnnotations;

namespace ArquitecturaHexagonalDDD.App.Infrastructure.EntrypointRest.Users.Request;

public class ChangePasswordHttpRequest
{
    [Required(ErrorMessage = "La contraseña actual es requerida")]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "La nueva contraseña es requerida")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "La nueva contraseña debe tener al menos 8 caracteres")]
    public string NewPassword { get; set; } = string.Empty;
}
