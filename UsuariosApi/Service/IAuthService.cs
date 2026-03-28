namespace UsuariosApi.Service;
using UsuariosApi.DTOs;

public interface IAuthService
{
    UsuarioResponseDto? Register(RegisterRequestDto dto);
    string Login(LoginRequestDto dto);
}
