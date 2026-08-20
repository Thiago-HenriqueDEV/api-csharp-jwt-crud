using CadastroTeste.Services;
using Microsoft.EntityFrameworkCore;
using CadastroTeste.Database;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IProdutosService, ProdutosDatabaseService>();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source = Produtos.db"));

var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.Run();

public class Produto
{
    public int Id {get; set;}

    [Required(ErrorMessage = "O campo nome é obrigatorio")]
    [StringLength(100, ErrorMessage ="Limite de caracteres atingido")]
    public string? Nome {get; set;}
     
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor minimo não foi preenchido")]
    public decimal Preco {get; set;}

    [Range(0.01, int.MaxValue, ErrorMessage = "O valor minimo não foi preenchido")]
    public int Estoque {get; set;}

}
