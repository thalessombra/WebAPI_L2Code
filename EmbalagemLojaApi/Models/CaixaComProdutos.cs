namespace EmbalagemLojaApi.Models
{
    public class CaixaComProdutos
    {
        public string CaixaId { get; set; }
        public List<string> Produtos { get; set; } = new List<string>();
        public int VolumeOcupado { get; set; }
        public int VolumeTotal { get; set; } = 100000;

    }
}
