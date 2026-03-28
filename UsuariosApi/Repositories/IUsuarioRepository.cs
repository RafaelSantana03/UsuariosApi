using UsuariosApi.Models;

namespace UsuariosApi.Repositories;

public interface IUsuarioRepository
{
    Usuario? ObterPorEmail(string email);
    void Criar (Usuario usuario);
    Usuario? ObterPorId(int id);
}
