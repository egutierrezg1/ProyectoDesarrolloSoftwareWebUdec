using System.ComponentModel.DataAnnotations;

namespace ArquitecturaHexagonalDDD.App.Infraestructure.Entrypoint-Rest.Users.Request;

public class UpdateUserHttpRequest
{
    [Required(ErrorMessage = "El nombre de usuario es requerido")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre de usuario debe tener entre 3 y 50 caracteres")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "El email es requerido")]
    [EmailAddress(ErrorMessage = "El formato del email no es válido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "El rol es requerido")]
    public string Role { get; set; } = string.Empty;
}
