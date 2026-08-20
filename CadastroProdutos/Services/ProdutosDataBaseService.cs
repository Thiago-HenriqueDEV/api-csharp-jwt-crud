using System;
using CadastroProdutos.Database;
namespace CadastroProdutos.Services;
using CadastroProdutos.Models;

public class ProdutosDataBaseService : IProdutosService
{

    private ApplicationDbContext BancoDeDados;
    public ProdutosDataBaseService(ApplicationDbContext bancoDeDados)
    {
       this.BancoDeDados = bancoDeDados;
    }
    public void Adicionar(Produto novoProduto)
    {
        BancoDeDados.Produtos.Add(novoProduto);
        BancoDeDados.SaveChanges();
    }

    public Produto Atualizar(int id, Produto produtoatualizado)
    {
       var produto = BancoDeDados.Produtos.FirstOrDefault(x => x.Id == id);
       if (produto is null)
        {
            return null;
        }

        produto.Nome = produtoatualizado.Nome;
        produto.Preco = produtoatualizado.Preco;
        produto.Estoque = produtoatualizado.Estoque;
        BancoDeDados.SaveChanges();
        return produto;

    }

    public Produto? ObterPorId(int id)
    {
        return BancoDeDados.Produtos.FirstOrDefault(x => x.Id == id);
    }

    public List<Produto> ObterTodosProdutos()
    {
        return BancoDeDados.Produtos.ToList();
    }

    public bool Remover(int id)
    {
        var produto = BancoDeDados.Produtos.FirstOrDefault(x => x.Id == id);
        if (produto is null)
        {
            return false;
        }

        BancoDeDados.Produtos.Remove(produto);
        BancoDeDados.SaveChanges();
        return true;
    }
}