using System.ComponentModel.DataAnnotations;

namespace ArquitecturaHexagonalDDD.App.Application.Users.Dto.Query;

public class GetUserByIdQuery
{
    [Required(ErrorMessage = "El ID del usuario es requerido")]
    public Guid Id { get; set; }
}
