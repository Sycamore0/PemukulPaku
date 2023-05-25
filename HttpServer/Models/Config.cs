using Newtonsoft.Json;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace HttpServer.Models
{
    public partial class CompareProtocolVersionBody
    {
        [JsonProperty("app_id")]
        public string AppId { get; set; }

        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("major")]
        public string Major { get; set; }

        [JsonProperty("minimum")]
        public string Minimum { get; set; }
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.