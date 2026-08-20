using CadastroProdutos.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CadastroProdutos.Models;

namespace CadastroProdutos.Controllers
{   [Authorize]
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
        public ActionResult<List<Produto>> Get()
        {
            return Ok(produtosService.ObterTodosProdutos());
        }

        [HttpGet("{id}")]
        public ActionResult<Produto> GetByID(int id)
        {
            var produto =  produtosService.ObterPorId(id);

            if (produto is null)
            {
                return NotFound($"Produto com o ID:{id} não encontrado");
            }

            return Ok(produto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Post(Produto NovoProduto)
        {
            produtosService.Adicionar(NovoProduto);

            return Created();
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public ActionResult Put(int id, Produto ProdutoAtualizado)
        {
            var produto = produtosService.Atualizar(id, ProdutoAtualizado);

            if(produto is null)
            {
                return NotFound($"Produto com o ID:{id}, não encontrado");
            }
            
           
            return Ok(produto);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")] 

        public ActionResult Delete(int id)
        {

            var Deletou = produtosService.Remover(id);
            

            if (Deletou == false)
            {
                return NotFound($"O produto com id{id}, não foi encontrado");
        
            }

            return NoContent();
      
        }

    





       











    }
}   