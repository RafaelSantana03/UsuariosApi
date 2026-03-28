using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuariosApi.DTOs;
using UsuariosApi.Models;
using UsuariosApi.Repositories;
using UsuariosApi.Service;

namespace UsuariosApi.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _repository;
    private readonly IConfiguration _configuration;

    public AuthService(IUsuarioRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    public UsuarioResponseDto Register(RegisterRequestDto dto)
    {
        // Verifica se o e-mail já está cadastrado
        var usuarioExistente = _repository.ObterPorEmail(dto.Email);
        if (usuarioExistente != null)
            throw new Exception("E-mail já cadastrado.");

        // Gera o hash da senha — nunca salva a senha pura
        var senhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);

        var usuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            SenhaHash = senhaHash
        };

        _repository.Criar(usuario);

        return new UsuarioResponseDto 
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            DataCadastro = usuario.DataCadastro
        };
    }

    public string Login(LoginRequestDto dto)
    {
        // Busca o usuário pelo e-mail
        var usuario = _repository.ObterPorEmail(dto.Email);
        if (usuario == null)
            throw new Exception("Credenciais inválidas.");

        // Verifica se a senha bate com o hash salvo no banco
        var senhaValida = BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash);
        if (!senhaValida)
            throw new Exception("Credenciais inválidas.");

        // Gera e retorna o token JWT
        return GerarToken(usuario);
    }

    private string GerarToken(Usuario usuario)
    {
        // Claims são informações do usuário que ficam dentro do token
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Email, usuario.Email)
        };

        // Cria a chave de assinatura com base na Key do appsettings
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Monta o token com validade de 8 horas
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(8),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}