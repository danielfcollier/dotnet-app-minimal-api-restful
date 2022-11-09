#nullable disable

using System.Text.Json.Serialization;

namespace Transaction
{
    public class Operation
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("origin")]
        public Source? Origin { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("destination")]
        public Source? Destination { get; set; }
    }

    public class Source
    {

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
    }
}