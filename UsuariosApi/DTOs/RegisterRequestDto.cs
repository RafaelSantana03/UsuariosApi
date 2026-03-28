using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace UsuariosApi.DTOs;

public class RegisterRequestDto
{
    [Required(ErrorMessage = "O Nome é obrigatório.")]
    [MinLength(3, ErrorMessage = "O Nome deve conter pelo menos 3 caracteres.")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]   
    public string? Email { get; set; }

    [Required(ErrorMessage = "A Senha é obrigatória.")]
    [MinLength(6, ErrorMessage = "A Senha deve conter pelo menos 6 caracteres.")]
    public string? Senha { get; set; }

}
