using System;
using CadastroProdutos.Models;

namespace CadastroProdutos.Services;

public interface IProdutosService
{
    public List<Produto> ObterTodosProdutos();

    public Produto ObterPorId (int id);

    public void Adicionar (Produto novoProduto);

    public Produto Atualizar (int id, Produto produtoatualizado);

    public bool Remover(int id);


}
