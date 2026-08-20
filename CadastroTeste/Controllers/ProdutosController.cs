using CadastroTeste.Services;
using Microsoft.AspNetCore.Mvc;
namespace CadastroTeste.Controllers




{
    [Route("api/[controller]")]
    [ApiController]


    public class ProdutosController : ControllerBase
    
    {
        private IProdutosService produtosService;
        public ProdutosController(IProdutosService produtosService)
        {
            this.produtosService = produtosService;
        }
    


       [HttpGet]
        public ActionResult<List<Produto>> GetActionResult()
        {
            return Ok(produtosService.ObterTodosProdutos());
        }

        [HttpGet("{id}")]
        public ActionResult<Produto> GetByID(int id)
        {
            var produto = produtosService.ObterPorId(id);

            if (produto is null)
            {
                return NotFound($"Produto com ID:{id}, não encontrado");
            }
            return Ok(produto);
        }
        
        [HttpPost]
        public ActionResult Post(Produto NovoProduto)
        {
            produtosService.Adicionar(NovoProduto);
            return Created();

        }

        [HttpPut("{id}")]
        public ActionResult Put (int id, Produto produtoAtualizado)
        {
            
            var produto = produtosService.Atualizar(id, produtoAtualizado);
            if(produto is null)
            {
                return NotFound($"Produto com o ID:{id}, não encontrado");
            }

            produto.Nome = produtoAtualizado.Nome;
            produto.Preco = produtoAtualizado.Preco;
            produto.Estoque = produtoAtualizado.Estoque;
            return Ok(produto);
        }

        [HttpDelete("{id}")]

        public ActionResult Delete (int id)
        {
           var Deletou = produtosService.Remover(id);
           if (Deletou == false)
            {
                return NotFound($"Produto com o ID:{id}, não encontrado");
            }

            return NoContent();
        }







    }
}