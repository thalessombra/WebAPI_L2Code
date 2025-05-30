using System.Text.Json.Serialization;

namespace EmbalagemLojaApi.Models
{
    public class CaixaSaida
    {
        [JsonPropertyName("caixa_id")]
        public string Caixa_Id { get; set; }

        [JsonPropertyName("produtos")]
        public List<string> Produtos { get; set; } = new();
        public string Observacao { get; internal set; }
    }

}
