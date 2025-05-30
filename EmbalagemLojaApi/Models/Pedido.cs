using System.Collections.Generic;
using System.Text.Json.Serialization;
using EmbalagemLojaApi.Models;

namespace EmbalagemLojaApi.Models
{
    public class Pedido
    {
        [JsonPropertyName("pedido_id")]
        public int PedidoId { get; set; }
        [JsonPropertyName("produtos")]
        public List<Produto> Produtos { get; set; }
    }
}
