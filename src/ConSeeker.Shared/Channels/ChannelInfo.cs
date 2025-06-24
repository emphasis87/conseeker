using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConSeeker.Shared.Channels
{
    public class ChannelInfo
    {
        [JsonPropertyName("channel")]
        public string Channel { get; set; }
        
        [JsonPropertyName("cons")]
        public List<ConventionInfo> Conventions { get; set; }
    }
}
