using Microsoft.AspNetCore.Builder;  // Importa o namespace para funcionalidades de construção de aplicativos web
using Microsoft.Extensions.DependencyInjection;  // Importa o namespace para injeção de dependências
using Microsoft.Extensions.Hosting;  // Importa o namespace para funcionalidades de hospedagem de aplicativos
using Microsoft.EntityFrameworkCore;  // Importa o namespace para funcionalidades do Entity Framework Core
using Npgsql.EntityFrameworkCore.PostgreSQL;  // Importa o provedor de EF Core para PostgreSQL
using MyPokedexAPI.Data;  // Importa o namespace para o contexto da base de dados
using Microsoft.AspNetCore.Authentication.JwtBearer;  // Importa o namespace para autenticação JWT
using Microsoft.IdentityModel.Tokens;  // Importa o namespace para funcionalidades de validação de tokens
using System.Text;  // Importa o namespace para manipulação de strings
using System.Text.Json.Serialization;  // Importa o namespace para serialização JSON

// Adiciona serviços ao contêiner.
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var connectionString = "Host=localhost;Database=MyPokedex;Username=postgres;Password=omfgnoob24413";  // Define a string de conexão com o banco de dados PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>  // Configura o contexto da base de dados para usar PostgreSQL
    options.UseNpgsql(connectionString));

// Configuração de CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")  // Define a origem permitida para CORS
              .AllowAnyHeader()  // Permite qualquer cabeçalho
              .AllowAnyMethod()  // Permite qualquer método
              .AllowCredentials();  // Permite credenciais
    });
});

// Configuração JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;  // Define o esquema de autenticação padrão
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  // Define o esquema de desafio padrão
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,  // Valida o emissor do token
        ValidateAudience = true,  // Valida a audiência do token
        ValidateLifetime = true,  // Valida o tempo de vida do token
        ValidateIssuerSigningKey = true,  // Valida a chave de assinatura do emissor
        ValidIssuer = builder.Configuration["Jwt:Issuer"],  // Define o emissor válido
        ValidAudience = builder.Configuration["Jwt:Audience"],  // Define a audiência válida
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))  // Define a chave de assinatura
    };
});

// Adicionando autorização
builder.Services.AddAuthorization();

var app = builder.Build();

// Configura o pipeline de requisição HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  // Usa a página de exceção do desenvolvedor em ambiente de desenvolvimento
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors();  // Habilita o middleware de CORS
app.UseAuthentication();  // Habilita o middleware de autenticação
app.UseAuthorization();  // Habilita o middleware de autorização

app.MapControllers();

app.Run();
