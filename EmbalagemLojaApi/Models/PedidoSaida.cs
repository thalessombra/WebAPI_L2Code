using System.Text.Json.Serialization;

namespace EmbalagemLojaApi.Models
{
    public class PedidoSaida
    {
        [JsonPropertyName("pedidos_id") ]
        public int Pedidos_Id { get; set; }

        [JsonPropertyName("caixas")]
        public List<CaixaSaida> Caixas { get; set; } = new();
    }
}
