using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmbalagemLojaApi.Data;
using EmbalagemLojaApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmbalagemLojaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class PedidosController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Caixas fixas para seu processamento customizado
        private List<Caixa> caixasDisponiveis = new List<Caixa>
        {
            new Caixa { CaixaId = "Caixa 1", Dimensoes = new Dimensoes { Altura = 30, Largura = 40, Comprimento = 80 } },
            new Caixa { CaixaId = "Caixa 2", Dimensoes = new Dimensoes { Altura = 80, Largura = 50, Comprimento = 40 } },
            new Caixa { CaixaId = "Caixa 3", Dimensoes = new Dimensoes { Altura = 50, Largura = 80, Comprimento = 60 } }
        };

        public PedidosController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.Produtos)
                .ToListAsync();

            return Ok(pedidos);
        }

        [Authorize]
        [HttpDelete("{pedidoId}")]
        public async Task<IActionResult> DeletePedido(int pedidoId)
        {
            var pedido = await _context.Pedidos.FindAsync(pedidoId);
            if (pedido == null)
                return NotFound();

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize]
        [HttpPost]
        public IActionResult ProcessarPedidos([FromBody] EntradaPedidos entrada)
        {
            if (entrada?.Pedidos == null || !entrada.Pedidos.Any())
                return BadRequest("A lista está vazia ou inválida.");

            var resposta = new List<PedidoSaida>();

            // Função para calcular volume
            int CalculaVolume(Dimensoes d) => d.Altura * d.Largura * d.Comprimento;

            foreach (var pedido in entrada.Pedidos)
            {
                var caixas = new List<CaixaSaida>();

                // Caixas abertas para o pedido: caixa, capacidade restante e a saída dela
                var caixasAbertas = new List<(Caixa caixa, int capacidadeRestante, CaixaSaida caixaSaida)>();

                foreach (var produto in pedido.Produtos)
                {
                    int volumeProduto = CalculaVolume(produto.Dimensoes);

                    // Tenta encaixar o produto numa caixa já aberta que tenha espaço
                    var caixaExistente = caixasAbertas.FirstOrDefault(c => c.capacidadeRestante >= volumeProduto);

                    if (caixaExistente.caixa != null)
                    {
                        caixaExistente.caixaSaida.Produtos.Add(produto.ProdutoID);
                        // Atualiza a capacidade restante
                        var index = caixasAbertas.IndexOf(caixaExistente);
                        caixasAbertas[index] = (caixaExistente.caixa, caixaExistente.capacidadeRestante - volumeProduto, caixaExistente.caixaSaida);
                    }
                    else
                    {
                        // Abre uma nova caixa disponível que comporte o produto
                        var caixaParaAbrir = caixasDisponiveis.FirstOrDefault(c => CalculaVolume(c.Dimensoes) >= volumeProduto);

                        if (caixaParaAbrir != null)
                        {
                            var novaCaixaSaida = new CaixaSaida
                            {
                                Caixa_Id = caixaParaAbrir.CaixaId,
                                Produtos = new List<string> { produto.ProdutoID }
                            };
                            caixasAbertas.Add((caixaParaAbrir, CalculaVolume(caixaParaAbrir.Dimensoes) - volumeProduto, novaCaixaSaida));
                        }
                        else
                        {
                            // Produto não cabe em nenhuma caixa disponível
                            caixas.Add(new CaixaSaida
                            {
                                Caixa_Id = null,
                                Produtos = new List<string> { produto.ProdutoID },
                                Observacao = "Produto não cabe em nenhuma caixa disponível."
                            });
                        }
                    }
                }

                // Junta todas as caixas abertas à resposta
                caixas.AddRange(caixasAbertas.Select(c => c.caixaSaida));

                resposta.Add(new PedidoSaida
                {
                    Pedidos_Id = pedido.PedidoId,
                    Caixas = caixas
                });
            }

            return Ok(new
            {
                mensagem = "A requisição foi recebida com sucesso",
                pedidosOrganizados = resposta
            });
        }
    }
}
