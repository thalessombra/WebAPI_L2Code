using System.Collections.Generic;
using System.Linq;
using EmbalagemLojaApi.Models;

namespace EmbalagemLojaApi.Services
{
    public class OrganizadoraDeCaixas
    {
        private readonly List<Caixa> _caixasDisponiveis;

        public OrganizadoraDeCaixas(List<Caixa> caixasDisponiveis)
        {
            _caixasDisponiveis = caixasDisponiveis;
        }

        public List<Caixa> OrganizarProdutos(List<Produto> produtos)
        {
            foreach (var produto in produtos)
            {
                
                var caixa = _caixasDisponiveis
                    .FirstOrDefault(c => c.VolumeDisponivel >= produto.Dimensoes.Volume);

                if (caixa != null)
                {
                    caixa.Produtos.Add(produto);
                }
                else
                {
                }
            }

            return _caixasDisponiveis.Where(c => c.Produtos.Any()).ToList();
        }
    }
}
