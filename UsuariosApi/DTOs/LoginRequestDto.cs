using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.DTOs;

public class LoginRequestDto
{
    [Required(ErrorMessage = "O Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "A Senha é obrigatória.")]
    public string? Senha { get; set; }
}
