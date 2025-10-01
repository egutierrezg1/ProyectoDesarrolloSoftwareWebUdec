using System.ComponentModel.DataAnnotations;

namespace ArquitecturaHexagonalDDD.App.Application.Users.Dto.Command;

public class ChangePasswordCommand
{
    [Required(ErrorMessage = "El ID del usuario es requerido")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "La contraseña actual es requerida")]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "La nueva contraseña es requerida")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "La nueva contraseña debe tener al menos 8 caracteres")]
    public string NewPassword { get; set; } = string.Empty;
}
