using System;
using CadastroTeste.Database;

namespace CadastroTeste.Services;

public class ProdutosDatabaseService : IProdutosService
{

    private ApplicationDbContext BancoDeDados;

    public ProdutosDatabaseService(ApplicationDbContext bancoDeDados)
    {
        this.BancoDeDados = bancoDeDados;
    }

 
    public void Adicionar(Produto novoProduto)
    {
        ValidarProduto(novoProduto);
        BancoDeDados.Produtos.Add(novoProduto);
        BancoDeDados.SaveChanges();
    }

    public Produto? Atualizar(int id, Produto produtoatualizado)
    {
        var produto =  BancoDeDados.Produtos.FirstOrDefault(x => x.Id == id);
        if(produto is null)
        {
            return null;
        }

        produto.Nome = produtoatualizado.Nome;
        produto.Estoque = produtoatualizado.Estoque;
        produto.Preco = produtoatualizado.Preco;
        
        BancoDeDados.SaveChanges();
        return produto;
    }

    public Produto? ObterPorId(int id)
    {
        return BancoDeDados.Produtos.FirstOrDefault(x => x.Id ==id);
        
    }

    public List<Produto> ObterTodosProdutos()
    {
        return BancoDeDados.Produtos.ToList();
    }

    public bool Remover(int id)
    {
        var produto = BancoDeDados.Produtos.FirstOrDefault(x => x.Id == id);
        if(produto is null)
        {
            return false;
        }

        BancoDeDados.Produtos.Remove(produto);
        BancoDeDados.SaveChanges();
        return true;

    }

    private void ValidarProduto(Produto produto)
    {
        if(produto.Nome == "Penis")
        {
            throw new Exception ("Produto com o nome cadastrado, não permitido");
        }

        if(produto.Estoque > 1000)
        {
            throw new Exception ("Produto com estoque abaixo de 1000");
        }
    }
}
