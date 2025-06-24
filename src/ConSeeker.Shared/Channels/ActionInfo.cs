using System.Text.Json.Serialization;

namespace ConSeeker.Shared.Channels
{
    public class ActionInfo
    {
        [JsonPropertyName("line")]
        public string Line { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("authors")]
        public string Authors { get; set; }

        [JsonPropertyName("start")]
        public DateTime Start { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
