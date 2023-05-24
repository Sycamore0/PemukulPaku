using Newtonsoft.Json;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace HttpServer.Models
{
    public partial class QueryDispatch
    {
        [JsonProperty("region_list")]
        public Region[] RegionList { get; set; }

        [JsonProperty("retcode")]
        public int Retcode { get; set; }
    }

    public partial class QueryGateway
    {
        [JsonProperty("account_url")]
        public string AccountUrl { get; set; }

        [JsonProperty("account_url_backup")]
        public string AccountUrlBackup { get; set; }

        [JsonProperty("asset_bundle_url_list")]
        public string[] AssetBundleUrlList { get; set; }

        [JsonProperty("ex_audio_and_video_url_list")]
        public object[] ExAudioAndVideoUrlList { get; set; }

        [JsonProperty("ex_resource_url_list")]
        public string[] ExResourceUrlList { get; set; }

        [JsonProperty("ext")]
        public Ext Ext { get; set; }

        [JsonProperty("gameserver")]
        public Gameserver Gameserver { get; set; }

        [JsonProperty("gateway")]
        public Gameserver Gateway { get; set; }

        [JsonProperty("is_data_ready")]
        public bool IsDataReady { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("oaserver_url")]
        public string OaserverUrl { get; set; }

        [JsonProperty("region_name")]
        public string RegionName { get; set; }

        [JsonProperty("retcode")]
        public int Retcode { get; set; }

        [JsonProperty("server_cur_time")]
        public long ServerCurTime { get; set; }

        [JsonProperty("server_cur_timezone")]
        public long ServerCurTimezone { get; set; }

        [JsonProperty("server_ext")]
        public ServerExt ServerExt { get; set; }
    }

    public partial class Ext
    {
        [JsonProperty("ai_use_asset_boundle")]
        public string AiUseAssetBoundle { get; set; }

        [JsonProperty("apm_log_dest")]
        public string ApmLogDest { get; set; }

        [JsonProperty("apm_log_level")]
        public string ApmLogLevel { get; set; }

        [JsonProperty("apm_switch")]
        public string ApmSwitch { get; set; }

        [JsonProperty("apm_switch_crash")]
        public string ApmSwitchCrash { get; set; }

        [JsonProperty("apm_switch_game_log")]
        public string ApmSwitchGameLog { get; set; }

        [JsonProperty("block_error_dialog")]
        public string BlockErrorDialog { get; set; }

        [JsonProperty("data_use_asset_boundle")]
        public string DataUseAssetBoundle { get; set; }
        
        [JsonProperty("enable_watermark")]
        public string EnableWatermark { get; set; }

        [JsonProperty("elevator_model_path")]
        public string ElevatorModelPath { get; set; }

        [JsonProperty("ex_audio_and_video_url_list")]
        public string[] ExAudioAndVideoUrlList { get; set; }

        [JsonProperty("ex_res_buff_size")]
        public string ExResBuffSize { get; set; }

        [JsonProperty("ex_res_pre_publish")]
        public string ExResPrePublish { get; set; }

        [JsonProperty("ex_res_use_http")]
        public string ExResUseHttp { get; set; }

        [JsonProperty("ex_resource_url_list")]
        public string[] ExResourceUrlList { get; set; }
        
        [JsonProperty("forbid_recharge")]
        public string ForbidRecharge { get; set; }

        [JsonProperty("is_checksum_off")]
        public string IsChecksumOff { get; set; }
        
        [JsonProperty("is_xxxx")]
        public string IsXxxx { get; set; }

        [JsonProperty("mtp_switch")]
        public string MtpSwitch { get; set; }

        [JsonProperty("network_feedback_enable")]
        public string NetworkFeedbackEnable { get; set; }

        [JsonProperty("offline_report_switch")]
        public string OfflineReportSwitch { get; set; }

        [JsonProperty("res_use_asset_boundle")]
        public string ResUseAssetBoundle { get; set; }

        [JsonProperty("show_bulletin_button")]
        public string ShowBulletinButton { get; set; }

        [JsonProperty("show_bulletin_empty_dialog_bg")]
        public string ShowBulletinEmptyDialogBg { get; set; }

        [JsonProperty("show_version_text")]
        public string ShowVersionText { get; set; }

        [JsonProperty("update_streaming_asb")]
        public string UpdateStreamingAsb { get; set; }

        [JsonProperty("use_multy_cdn")]
        public string UseMultyCdn { get; set; }
    }

    public partial class Gameserver
    {
        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("port")]
        public uint Port { get; set; }
    }

    public partial class Region
    {
        [JsonProperty("dispatch_url")]
        public string DispatchUrl { get; set; }

        [JsonProperty("ext")]
        public Ext Ext { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("retcode")]
        public int Retcode { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }

    public partial class ServerExt
    {
        [JsonProperty("cdkey_url")]
        public string CdkeyUrl { get; set; }

        [JsonProperty("mihoyo_sdk_env")]
        public string MihoyoSdkEnv { get; set; }
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.