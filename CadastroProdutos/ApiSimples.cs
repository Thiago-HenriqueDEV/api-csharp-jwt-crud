using CadastroProdutos.Services;
using Microsoft.EntityFrameworkCore;
using CadastroProdutos.Database;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using CadastroProdutos.Models;

var builder = WebApplication.CreateBuilder(args);


// Suporte aos Controllers (Essencial para o LoginController funcionar)
builder.Services.AddControllers();

// Configuração do Banco de Dados SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlite("Data Source=Produtos.db"));

// Injeção de Dependência dos Serviços
builder.Services.AddScoped<IProdutosService, ProdutosDataBaseService>();

// Configuração do Swagger com suporte a Token JWT 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = Microsoft.OpenApi.ParameterLocation.Header,
        Type = Microsoft.OpenApi.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "Insira apenas o seu token JWT (Sem 'Bearer')"
    });
    x.AddSecurityRequirement(document => new ()
    {
        [new Microsoft.OpenApi.OpenApiSecuritySchemeReference("Bearer", document)] = []
    } );


});

// Configuração de Autenticação e Autorização via Token JWT
var jwtConfig = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtConfig["Key"] ?? "");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtConfig["Issuer"],
        ValidAudience = jwtConfig["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});



var app = builder.Build();

// Configurando o ambiente de Desenvolvimento (Swagger)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Configuração de autenticação e autorização 
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();




// Variável temporária para testes em armazenamento temporario
var produtos = new List<Produto>()
{
    new Produto() { Id = 1, Nome = "Fone", Preco = 10.0M, Estoque = 100},
    new Produto() { Id = 2, Nome = "Mouse", Preco = 20.0M, Estoque = 200}
};

app.MapGet("/produtoslist", () =>
{
    return produtos;
}).RequireAuthorization();

app.MapGet("/produtos/{id}", (int id) =>
{
    var produto = produtos.FirstOrDefault(x => x.Id == id);
    return produto is not null 
        ? Results.Ok(produto)
        : Results.NotFound($"Produto com o ID:{id} não encontrado");
});

app.MapPost("/produtos", (Produto novoProduto) =>
{
    produtos.Add(novoProduto);
    return Results.Created();
});

app.MapPut("/produtos/{id}", (int id, Produto produtoAtualizado) =>
{
    var produto = produtos.FirstOrDefault(x => x.Id == id);
    if(produto is null)
    {
        return Results.NotFound($"Produto com o ID:{id} não encontrado");
    }

    produto.Nome = produtoAtualizado.Nome;
    produto.Preco = produtoAtualizado.Preco;
    produto.Estoque = produtoAtualizado.Estoque;

    return Results.Ok(produto);
});

app.MapDelete("/produtos/{id}", (int id) =>
{
    var produto = produtos.FirstOrDefault(x => x.Id == id);
    if(produto is null)
    {
        return Results.NotFound($"Produto com o ID:{id} não encontrado");
    }

    produtos.Remove(produto);
    return Results.Ok($"Produto com o ID:{id} foi removido com sucesso");
});



// Inicia a aplicação
app.Run();








