using UsuariosApi.Data;
using UsuariosApi.Models;

namespace UsuariosApi.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;
    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public Usuario? ObterPorEmail(string email)
    {
        return _context.Usuarios.FirstOrDefault(u => u.Email == email);
    }
    public void Criar(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }

    public Usuario? ObterPorId(int id)
    {
        return _context.Usuarios.Find(id);
    }
}
