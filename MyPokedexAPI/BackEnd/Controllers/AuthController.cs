using Microsoft.AspNetCore.Mvc;  // Importa o namespace para funcionalidades do MVC no ASP.NET Core
using Microsoft.IdentityModel.Tokens;  // Importa o namespace para funcionalidades relacionadas a tokens de segurança
using MyPokedexAPI.Data;  // Importa o namespace para acesso ao contexto da base de dados
using MyPokedexAPI.Models;  // Importa o namespace para modelos da aplicação
using System.IdentityModel.Tokens.Jwt;  // Importa o namespace para funcionalidades relacionadas a JWT (JSON Web Tokens)
using System.Security.Claims;  // Importa o namespace para funcionalidades relacionadas a declarações de segurança (claims)
using System.Text;  // Importa o namespace para manipulação de strings
using Microsoft.EntityFrameworkCore;  // Importa o namespace para funcionalidades do Entity Framework Core

namespace MyPokedexAPI.Controllers  // Define o namespace para o controlador da API
{
    [ApiController]  // Indica que esta classe é um controlador de API
    [Route("api/[controller]")]  // Define a rota para aceder a este controlador
    public class AuthController : ControllerBase  // Define a classe AuthController que herda de ControllerBase
    {
        private readonly ApplicationDbContext _context;  // Campo para o contexto da base de dados
        private readonly IConfiguration _configuration;  // Campo para a configuração da aplicação

        public AuthController(ApplicationDbContext context, IConfiguration configuration)  // Construtor que inicializa os campos _context e _configuration
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]  // Define um endpoint HTTP POST na rota "register"
        public async Task<IActionResult> Register(UserRegisterDTO registerDto)  // Método para registar um novo utilizador
        {
            if (registerDto == null)  // Verifica se o DTO de registo é nulo
            {
                return BadRequest("Pedido do cliente inválido");  // Retorna um erro de pedido inválido
            }

            var userExists = await _context.Users.AnyAsync(u => u.Username == registerDto.Username || u.Email == registerDto.Email);  // Verifica se o utilizador já existe
            if (userExists)  // Se o utilizador já existe
            {
                return Conflict("Nome de utilizador ou email já existe.");  // Retorna um erro de conflito
            }

            var user = new User  // Cria um novo objeto User
            {
                Name = registerDto.Name,
                Username = registerDto.Username,
                Password = registerDto.Password,  // A senha deve ser hash e salted em um cenário real
                Email = registerDto.Email,
                CreationDate = DateTime.UtcNow,
                IsActive = true
            };

            _context.Users.Add(user);  // Adiciona o novo utilizador ao contexto
            await _context.SaveChangesAsync();  // Salva as alterações na base de dados

            return Ok("Utilizador registado com sucesso");  // Retorna uma resposta de sucesso
        }

        [HttpPost("login")]  // Define um endpoint HTTP POST na rota "login"
        public async Task<IActionResult> Login(UserLoginDTO loginDto)  // Método para autenticar um utilizador
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username && u.Password == loginDto.Password);  // Procura o utilizador na base de dados
            if (user == null)  // Se o utilizador não for encontrado
            {
                return Unauthorized("Nome de utilizador ou palavra-passe inválidos");  // Retorna um erro de não autorizado
            }

            var token = GenerateJwtToken(user);  // Gera um token JWT para o utilizador
            return Ok(new { Token = token, UserId = user.Id });  // Retorna o token e o ID do utilizador
        }

        private string GenerateJwtToken(User user)  // Método para gerar um token JWT
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));  // Cria a chave de segurança
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);  // Cria as credenciais de assinatura

            var claims = new[]  // Cria as claims para o token
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(  // Cria o token JWT
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);  // Retorna o token como string
        }
    }
}
