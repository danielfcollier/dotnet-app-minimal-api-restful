#nullable disable

using System.Text.Json.Serialization;

namespace Model
{
    public class Account
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
    }
}