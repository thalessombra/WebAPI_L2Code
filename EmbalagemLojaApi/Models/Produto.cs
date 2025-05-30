using System.Text.Json.Serialization;

namespace EmbalagemLojaApi.Models
{
    public class Produto
    {
        [JsonPropertyName("produto_id")]
        public string ProdutoID { get; set; }

        [JsonPropertyName("dimensoes")]
        public Dimensoes Dimensoes { get; set; }
    }
}
