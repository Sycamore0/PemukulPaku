using Newtonsoft.Json;


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace HttpServer.Models
{
    public partial class RiskyCheck
    {
        [JsonProperty("retcode")]
        public int Retcode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public DataScheme Data { get; set; }

        public partial class DataScheme
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("action")]
            public string Action { get; set; }

            [JsonProperty("geetest")]
            public object? Geetest { get; set; }
        }
    }

    public partial class GranterLoginBody
    {
        [JsonProperty("app_id")]
        public int AppId { get; set; }

        [JsonProperty("channel_id")]
        public int ChannelId { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("device")]
        public string Device { get; set; }

        [JsonProperty("sign")]
        public string Sign { get; set; }

        public partial class GranterLoginBodyData
        {
            [JsonProperty("uid")]
            public string Uid { get; set; }

            [JsonProperty("guest")]
            public bool Guest { get; set; }

            [JsonProperty("token")]
            public string Token { get; set; }
        }
    }

    public partial class ShieldVerifyBody
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("uid")]
        public string Uid { get; set; }
    }

    public partial class ShieldLoginBody
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("is_crypto")]
        public bool IsCrypto { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }

    public partial class ShieldLoginResponse
    {
        [JsonProperty("data")]
        public ShieldLoginResponseData Data { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("retcode")]
        public long Retcode { get; set; }

        public partial class ShieldLoginResponseData
        {
            [JsonProperty("account")]
            public ShieldLoginResponseDataAccount? Account { get; set; }

            [JsonProperty("device_grant_required")]
            public bool DeviceGrantRequired { get; set; }

            [JsonProperty("reactivate_required")]
            public bool ReactivateRequired { get; set; }

            [JsonProperty("realname_operation")]
            public string RealnameOperation { get; set; }

            [JsonProperty("realperson_required")]
            public bool RealpersonRequired { get; set; }

            [JsonProperty("safe_moblie_required")]
            public bool SafeMoblieRequired { get; set; }

            public partial class ShieldLoginResponseDataAccount
            {
                [JsonProperty("apple_name")]
                public string AppleName { get; set; }

                [JsonProperty("area_code")]
                public string AreaCode { get; set; }

                [JsonProperty("country")]
                public string Country { get; set; }

                [JsonProperty("device_grant_ticket")]
                public string DeviceGrantTicket { get; set; }

                [JsonProperty("email")]
                public string Email { get; set; }

                [JsonProperty("facebook_name")]
                public string FacebookName { get; set; }

                [JsonProperty("game_center_name")]
                public string GameCenterName { get; set; }

                [JsonProperty("google_name")]
                public string GoogleName { get; set; }

                [JsonProperty("identity_card")]
                public string IdentityCard { get; set; }

                [JsonProperty("is_email_verify")]
                public string IsEmailVerify { get; set; }

                [JsonProperty("mobile")]
                public string Mobile { get; set; }

                [JsonProperty("name")]
                public string Name { get; set; }

                [JsonProperty("reactivate_ticket")]
                public string ReactivateTicket { get; set; }

                [JsonProperty("realname")]
                public string Realname { get; set; }

                [JsonProperty("safe_mobile")]
                public string SafeMobile { get; set; }

                [JsonProperty("sony_name")]
                public string SonyName { get; set; }

                [JsonProperty("steam_name")]
                public string SteamName { get; set; }

                [JsonProperty("tap_name")]
                public string TapName { get; set; }

                [JsonProperty("token")]
                public string Token { get; set; }

                [JsonProperty("twitter_name")]
                public string TwitterName { get; set; }

                [JsonProperty("uid")]
                public long Uid { get; set; }

                [JsonProperty("unmasked_email")]
                public string UnmaskedEmail { get; set; }

                [JsonProperty("unmasked_email_type")]
                public long UnmaskedEmailType { get; set; }
            }
        }
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.