# 🔐 Usuários API

API REST para cadastro e autenticação de usuários com **JWT (JSON Web Token)**, desenvolvida com **ASP.NET Core** e **Entity Framework Core**, seguindo boas práticas como Repository Pattern, DTOs, validações com Data Annotations e armazenamento seguro de senhas com BCrypt.

---

## 🚀 Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/)
- [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [JWT Bearer Authentication](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/jwt-authn)
- [BCrypt.Net](https://github.com/BcryptNet/bcrypt.net)
- [Swagger / OpenAPI](https://swagger.io/)

---

## 📋 Pré-requisitos

Antes de rodar o projeto, certifique-se de ter instalado:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)

---

## ⚙️ Como Rodar o Projeto

### 1. Clone o repositório

```bash
git clone https://github.com/RafaelSantana03/UsuariosApi.git
cd UsuariosApi
```

### 2. Configure a Connection String

No arquivo `appsettings.json`, ajuste a connection string de acordo com sua instância do SQL Server:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SUA_INSTANCIA;Database=UsuariosDb;Trusted_Connection=true;TrustServerCertificate=true;"
  },
  "Jwt": {
    "Key": "",
    "Issuer": "UsuariosApi",
    "Audience": "UsuariosApi"
  }
}
```

### 3. Configure a chave JWT com User Secrets

A chave JWT **não está no código** por segurança. Configure-a localmente com o recurso de User Secrets do .NET:

```bash
dotnet user-secrets init
dotnet user-secrets set "Jwt:Key" "sua-chave-secreta-com-minimo-32-caracteres"
```

> ⚠️ A chave deve ter no mínimo 32 caracteres para o algoritmo HS256.

### 4. Execute as Migrations

```bash
dotnet ef database update
```

### 5. Rode o projeto

```bash
dotnet run
```

### 6. Acesse o Swagger

```
https://localhost:{porta}/swagger
```

---

## 🛣️ Endpoints da API

Base URL: `/api/auth`

| Método | Rota | Descrição | Autenticação | Status de Sucesso |
|--------|------|-----------|--------------|-------------------|
| `POST` | `/api/auth/register` | Cadastra um novo usuário | ❌ Pública | `201 Created` |
| `POST` | `/api/auth/login` | Autentica e retorna o token JWT | ❌ Pública | `200 OK` |
| `GET` | `/api/auth/perfil` | Retorna os dados do usuário autenticado | ✅ Requer token | `200 OK` |

### Exemplo de body para Register

```json
{
  "nome": "Rafael Santana",
  "email": "rafael@email.com",
  "senha": "minhasenha123"
}
```

### Exemplo de body para Login

```json
{
  "email": "rafael@email.com",
  "senha": "minhasenha123"
}
```

### Resposta do Login

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

### Como usar o token no Swagger

1. Clique no botão **Authorize 🔓** no canto superior direito
2. Digite `Bearer ` seguido do token recebido no login
3. Clique em **Authorize**

### Resposta do Perfil

```json
{
  "id": "1",
  "nome": "Rafael Santana",
  "email": "rafael@email.com"
}
```

> 💡 Os dados do perfil são extraídos diretamente do token JWT — sem consultar o banco de dados.

---

### Regras de validação

| Campo | Regra |
|-------|-------|
| `Nome` | Obrigatório, mínimo 3 caracteres |
| `Email` | Obrigatório, formato válido de e-mail |
| `Senha` | Obrigatória, mínimo 6 caracteres |

---

## 🗂️ Estrutura de Pastas

```
UsuariosApi/
├── Controllers/
│   └── AuthController.cs       # Rotas de autenticação
├── Data/
│   └── AppDbContext.cs         # Contexto do Entity Framework
├── DTOs/
│   ├── LoginRequestDto.cs      # Dados recebidos no login
│   ├── RegisterRequestDto.cs   # Dados recebidos no cadastro
│   └── UsuarioResponseDto.cs   # Dados retornados pela API
├── Models/
│   └── Usuario.cs              # Modelo da entidade no banco
├── Repositories/
│   ├── IUsuarioRepository.cs   # Interface do repositório
│   └── UsuarioRepository.cs    # Implementação do repositório
├── Services/
│   ├── IAuthService.cs         # Interface do serviço de autenticação
│   └── AuthService.cs          # Lógica de autenticação e geração de JWT
├── appsettings.json
└── Program.cs
```

---

## 🔐 Segurança

- Senhas armazenadas com **BCrypt** — nunca em texto puro
- Chave JWT gerenciada via **.NET User Secrets** — fora do controle de versão
- Token JWT com validade de **8 horas**
- Rotas protegidas com `[Authorize]` — retornam `401 Unauthorized` sem token válido
- Mensagens de erro de login **genéricas** — evita exposição de dados (e-mail existe ou não)

---

## 📐 Padrões e Boas Práticas Aplicadas

- **Repository Pattern** — separação entre lógica de acesso a dados e regras de negócio
- **Service Layer** — lógica de autenticação isolada do controller
- **DTOs (Data Transfer Objects)** — controle do que entra e sai da API
- **Data Annotations** — validações declarativas no DTO de entrada
- **Injeção de Dependência** — uso nativo do ASP.NET Core
- **User Secrets** — gerenciamento seguro de configurações sensíveis
- **JWT Stateless** — dados do usuário nas Claims, sem consultas desnecessárias ao banco

---

## 👨‍💻 Autor

**Rafael Santana**  
[![GitHub](https://img.shields.io/badge/GitHub-RafaelSantana03-181717?style=flat&logo=github)](https://github.com/RafaelSantana03)
