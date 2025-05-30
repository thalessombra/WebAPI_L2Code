using System.ComponentModel.DataAnnotations.Schema;

namespace EmbalagemLojaApi.Models
{
    public class Caixa
    {
        public string CaixaId { get; set; }

        [NotMapped]
        public Dimensoes Dimensoes { get; set; }
        public List<Produto> Produtos { get; set; } = new List<Produto>();

        public int VolumeMaximo => Dimensoes.Volume;
        public int VolumeOcupado => Produtos.Sum(p => p.Dimensoes.Volume);
        public int VolumeDisponivel => VolumeMaximo - VolumeOcupado;
    }
}
