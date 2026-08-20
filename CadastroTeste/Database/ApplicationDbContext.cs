using System;
using Microsoft.EntityFrameworkCore;
using CadastroTeste.Services;
namespace CadastroTeste.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    //*Nossas tabelas do banco de dados
    public DbSet<Produto> Produtos { get; set;}
}