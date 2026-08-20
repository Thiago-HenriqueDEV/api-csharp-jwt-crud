using System;
using Microsoft.EntityFrameworkCore;
using CadastroProdutos.Services;
namespace CadastroProdutos.Database;
using CadastroProdutos.Models;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Produto> Produtos { get; set;}
}