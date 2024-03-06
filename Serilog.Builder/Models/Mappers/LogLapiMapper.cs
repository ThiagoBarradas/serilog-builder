using Newtonsoft.Json;

namespace Serilog.Builder.Models.Mappers
{
    [JsonObject]
    public class LogLapiMapper
    {
        [JsonProperty(PropertyName = "timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty(PropertyName = "machinename")]
        public string MachineName { get; set; }

        [JsonProperty(PropertyName = "severity")]
        public string Severity { get; set; }

        [JsonProperty(PropertyName = "productname")]
        public string ProductName { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "event")]
        public object Event { get; set; }
    }
}
