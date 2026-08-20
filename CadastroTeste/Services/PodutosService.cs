using System;
using CadastroTeste.Controllers;
using IProdutosService = CadastroTeste.Services.IProdutosService;

namespace CadastroTeste.Services;


public class ProdutosService : IProdutosService
{
     private static List<Produto> produtos = new List<Produto>()
       {
           new Produto() { Id = 1, Nome = "Computador", Preco = 2500.00M, Estoque = 122},
           new Produto() { Id = 2, Nome = "Mouse", Preco = 50.00M, Estoque = 500}
       };
    public List<Produto> ObterTodosProdutos()
    {
        return produtos;
    }

    public Produto? ObterPorId (int id)
    {
        return produtos.FirstOrDefault(x => x.Id == id);
    }

    public void Adicionar(Produto novoProduto)
    {
        produtos.Add(novoProduto);
        
    }

    public Produto? Atualizar (int id, Produto produtoAtualizado)
    {
        var produto = produtos.FirstOrDefault(x => x.Id == id);

        if (produto is null)
        {
            return null;
        }

        produto.Nome = produtoAtualizado.Nome;
        produto.Preco = produtoAtualizado.Preco;
        produto.Estoque = produtoAtualizado.Estoque;

        return (produto);
    } 

    public bool Remover(int id)
    {
        var produto = produtos.FirstOrDefault(x => x.Id == id);
        if (produto is null)
        {
            return false;
        }
        produtos.Remove(produto);
        return true;
    }
}       