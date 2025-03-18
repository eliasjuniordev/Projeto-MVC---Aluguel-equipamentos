using System.Text.Json.Serialization;

namespace AluguelEquipamentos.Negocio.Dtos
{
    public class EnderecoResponse
    {
        [JsonPropertyName("CEP")]
        public string? CEP { get; set; }
        [JsonPropertyName("Estado")]
        public string? State { get; set; }
        [JsonPropertyName("Cidade")]
        public string? City { get; set; }
        [JsonPropertyName("Regiao")]
        public string? Neighborhood { get; set; }

        [JsonPropertyName("Rua")]
        public string? Street { get; set; }
        [JsonIgnore]
        public string? Service { get; set; }
    }
}
