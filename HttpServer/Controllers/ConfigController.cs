using Common;
using HttpServer.Models;
using Newtonsoft.Json;

namespace HttpServer.Controllers
{
    public class ConfigController
    {
        public static void AddHandlers(WebApplication app)
        {
            app.Map("/{game_biz}/mdk/agreement/api/getAgreementInfos", (HttpContext ctx) =>
            {
                return ctx.Response.WriteAsJsonAsync(new { retcode = 0, message = "OK", data = new { marketing_agreements = Array.Empty<object>() } });
            });

            app.Map("/data_abtest_api/config/experiment/list", (ctx) =>
            {
                return ctx.Response.WriteAsJsonAsync(new 
                {
                    retcode = 0,
                    success = true,
                    message = "",
                    data = Array.Empty<object>()
                });
            });

            app.Map("/account/device/api/listNewerDevices", (ctx) =>
            {
                return ctx.Response.WriteAsJsonAsync(new
                {
                    data = new {
                        devices = Array.Empty<object>(),
                        latest_id = "0"
                    },
                    message = "OK",
                    retcode = 0
                });
            });

            app.Map("/{game_biz}/combo/granter/api/getConfig", (HttpContext ctx) =>
            {
                return ctx.Response.WriteAsJsonAsync(new
                {
                    retcode = 0,
                    message = "OK",
                    data = new {
                        protocol = true,
			            qr_enabled = false,
			            log_level = "DEBUG",
			            announce_url = $"https://{Global.config.Gameserver.Host}/bh3/announcement/",
			            push_alias_type = 2,
			            disable_ysdk_guard = false,
			            enable_announce_pic_popup = false,
			            app_name = "崩坏3-东南亚",
			            qr_enabled_apps = new { bbs = false, cloud = false },
			            qr_app_icons = new { app = "", bbs = "", cloud = "" },
			            qr_cloud_display_name = ""
		            }
	            });
            });
            
            app.Map("/game_weather/weather/get_weather", (HttpContext ctx) =>
            {
                Weather weatherData = new()
                {
                    Retcode = 0,
                    Message = "OK",
                    Data = new()
                    {
                        Timezone = 8,
                        Hourly = new()
                    }
                };

                Random random = new();
                for (int i = 0; i < 24; i++)
                {
                    DateTime time = DateTime.Now.Add(TimeSpan.FromHours(1) * i);
                    weatherData.Data.Hourly.Add(new() { Condition = 1, Date = time.ToString("yyyy-MM-dd"), Hour = time.Hour, Temp = random.Next(20, 30) });
                }

                ctx.Response.Headers.Add("Content-Type", "application/json");
                return ctx.Response.WriteAsync(JsonConvert.SerializeObject(weatherData));
            });

            app.Map("/{game_biz}/mdk/shield/api/loadConfig", (HttpContext ctx) =>
            {
                return ctx.Response.WriteAsJsonAsync(new
                {
                    retcode = 0,
                    message = "OK",
                    data = new
                    {
                        id = 16,
                        game_key = ctx.Request.Query["game_key"],
                        client = "PC",
                        identity = "I_IDENTITY",
                        guest = false,
                        ignore_versions = "",
                        scene = "S_NORMAL",
                        name = "崩坏3rd-东南亚",
                        disable_regist = false,
                        enable_email_captcha = false,
                        thirdparty = Array.Empty<string>(),
                        disable_mmt = false,
                        server_guest = false,
                        thirdparty_ignore = new { },
                        enable_ps_bind_account = false,
                        thirdparty_login_configs = new { },
                        initialize_firebase = false
                    }
                });
            });
            
            app.Map("/combo/box/api/config/sdk/combo", (HttpContext ctx) =>
            {
                return ctx.Response.WriteAsJsonAsync(new
                {
                    retcode = 0,
                    message = "OK",
                    data = new 
                    {
                        vals = new
                        {
                            list_price_tierv2_enable = "false",
				            network_report_config = new
                            {
                                enable = 0,
					            status_codes = new int[] { 206 },
					            url_paths = new string[] { "dataUpload", "red_dot" },
				            },
				            kibana_pc_config = new 
                            {
					            enable = 1,
					            level = "Debug",
					            modules = new string[] { "download" }
				            },
			            },
		            },
	            });
            });

            app.Map("/device-fp/api/getExtList", (ctx) =>
            {
                return ctx.Response.WriteAsJsonAsync(new
                {
                    retcode = 0,
                    message = "OK",
                    data = new
                    {
                        code = 200,
                        msg = "ok",
                        ext_list = new string[] {

                            "cpuName",
                            "deviceModel",
                            "deviceName",
                            "deviceType",
                            "deviceUID",
                            "gpuID",
                            "gpuName",
                            "gpuAPI",
                            "gpuVendor",
                            "gpuVersion",
                            "gpuMemory",
                            "osVersion",
                            "cpuCores",
                            "cpuFrequency",
                            "gpuVendorID",
                            "isGpuMultiTread",
                            "memorySize",
                            "screenSize",
                            "engineName",
                            "addressMAC"
                        },
                        pkg_list = Array.Empty<object>(),
                        pkg_str = "/vK5WTh5SS3SAj8Zm0qPWg=="
                    },
                });
            });

            app.Map("/device-fp/api/getFp", (ctx) =>
            {
                return ctx.Response.WriteAsJsonAsync(new
                {
                    data = new
                    {
                        code = 200,
                        device_fp = ctx.Request.Query["device_fp"],
                        msg = "ok",
                    },
                    message = "OK",
                    retcode = 0,
                });
            });

            app.Map("/report", (ctx) =>
            {
                return ctx.Response.WriteAsync("GET LOG");
            });

            app.Map("/admin/mi18n/{*remainder}", (ctx) =>
            {
                return ctx.Response.WriteAsJsonAsync(new
                {
                    version = 74
                });
            });

            app.Map("/sdk/dataUpload", (ctx) =>
            {
                return ctx.Response.WriteAsJsonAsync(new
                {
                    code = 0
                });
            });

#pragma warning disable CS8600, CS8602 // Converting null literal or possible null value to non-nullable type.
            app.MapPost("/{game_biz}/combo/granter/api/compareProtocolVersion", (ctx) =>
            {
                StreamReader Reader = new(ctx.Request.Body);
                CompareProtocolVersionBody Data = JsonConvert.DeserializeObject<CompareProtocolVersionBody>(Reader.ReadToEndAsync().Result);

                return ctx.Response.WriteAsJsonAsync(new
                {
                    retcode = 0,
                    message = "OK",
                    data = new
                    {
                        modified = true,
                        protocol = new 
                        {
                            id = 0,
                            app_id = Data.AppId,
                            language = Data.Language,
                            user_proto = "",
                            priv_proto = "",
                            major = 0,
                            minimum = 3,
                            create_time = "0",
                            teenager_proto = "",
                            third_proto = ""
                        }
                    }
                });
            });
#pragma warning restore CS8600, CS8602 // Converting null literal or possible null value to non-nullable type.
        }
    }
}
