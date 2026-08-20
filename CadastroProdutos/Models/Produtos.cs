using System;
using System.ComponentModel.DataAnnotations;

namespace CadastroProdutos.Models;


public class Produto
{
    public int Id { get; set; }
    public string? Nome { get; set; } 
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
}