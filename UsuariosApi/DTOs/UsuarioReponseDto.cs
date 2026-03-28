namespace UsuariosApi.DTOs;

public class UsuarioReponseDto
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public DateTime DataCadastro { get; set; }
}
