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

    public partial class Weather
    {
        [JsonProperty("retcode")]
        public int Retcode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public WeatherData Data { get; set; }
    }

    public partial class WeatherData
    {
        [JsonProperty("timezone")]
        public int Timezone { get; set; }

        [JsonProperty("hourly")]
        public List<HourlyData> Hourly { get; set; }

        public partial class HourlyData
        {
            [JsonProperty("condition")]
            public long Condition { get; set; }

            [JsonProperty("hour")]
            public long Hour { get; set; }

            [JsonProperty("date")]
            public string Date { get; set; }

            [JsonProperty("temp")]
            public long Temp { get; set; }
        }
    }

}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.