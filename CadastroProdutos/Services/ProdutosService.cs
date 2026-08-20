using System;
using IProdutosService = CadastroProdutos.Services.IProdutosService;
using CadastroProdutos.Models;

namespace ProdutosService.Services;

public class ProdutosService : IProdutosService
{
    private static List<Produto> produtos = new List<Produto>
        {
         new Produto { Id = 1, Nome = "Mouse", Preco =  50.00m, Estoque = 38},
         new Produto { Id = 2, Nome = "Teclado", Preco = 150.00m, Estoque = 34},
         new Produto { Id = 3, Nome = "Monitor", Preco = 1000.00m, Estoque = 12}
        };

    public List<Produto> ObterTodosProdutos()
         {
          return produtos;
         } 
    public Produto? ObterPorId (int id)
    {
        return produtos.FirstOrDefault(x => x.Id == id);
    }

    public void Adicionar (Produto novoProduto)
    {
        produtos.Add(novoProduto);

    }
    public Produto Atualizar (int id, Produto produtoatualizado)
    {
        var produto = produtos.FirstOrDefault(x => x.Id == id);

        if (produto is null)
        {
            return null;
        }

        produto.Nome = produtoatualizado.Nome;
        produto.Preco = produtoatualizado.Preco;
        produto.Estoque = produtoatualizado.Estoque;

        return produto;

    }

    public bool Remover(int id)
    {


        var produto = produtos.FirstOrDefault(x => x.Id == id);
        if(produto is null)
        {
            return false;
        }

        produtos.Remove(produto);
        return true;
        
    }

}

