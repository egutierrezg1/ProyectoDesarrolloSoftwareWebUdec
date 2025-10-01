using System.ComponentModel.DataAnnotations;

namespace ArquitecturaHexagonalDDD.App.Application.Users.Dto.Command;

public class DeleteUserCommand
{
    [Required(ErrorMessage = "El ID del usuario es requerido")]
    public Guid Id { get; set; }
}
