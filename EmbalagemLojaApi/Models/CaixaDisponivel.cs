namespace EmbalagemLojaApi.Models
{
    public class CaixaDisponivel
    {
        public string CaixaId { get; set; }
        public int VolumeMaximo { get; set; }

        public CaixaDisponivel(string id, int volume)
        {
            CaixaId = id;
            VolumeMaximo = volume;
        }
    }
}
