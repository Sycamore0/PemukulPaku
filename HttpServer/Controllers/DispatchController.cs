using HttpServer.Models;
using Newtonsoft.Json;
using Common;
using System.Text.RegularExpressions;

namespace HttpServer.Controllers
{
    public class DispatchController
    {
        public static void AddHandlers(WebApplication app)
        {
            app.Map("/query_dispatch", (ctx) =>
            {
                QueryDispatch rsp = new()
                {
                    Retcode = 0,
                    RegionList = new Region[] {
                        new Region() {
                            Retcode = 0,
                            DispatchUrl = $"http://{Global.config.Gameserver.Host}/query_gateway",
                            Name = Global.config.Gameserver.RegionName,
                            Title = "",
                            Ext = GetExt(ctx.Request.Query["version"].ToString())
                        }
                    }
                };
                ctx.Response.Headers.Add("Content-Type", "application/json");
                return ctx.Response.WriteAsync(JsonConvert.SerializeObject(rsp));
            });

            app.Map("/query_gateway", (ctx) =>
            {
                string Version = ctx.Request.Query["version"].ToString();
                Gameserver Gameserver = new()
                {
                    Ip = Global.config.Gameserver.Host,
                    Port = Global.config.Gameserver.Port
                };

                QueryGateway rsp = new()
                {
                    Retcode = 0,
                    Msg = "",
                    RegionName = Global.config.Gameserver.RegionName,
                    AccountUrl = $"http://{Global.config.Gameserver.Host}/account",
                    AccountUrlBackup = $"http://{Global.config.Gameserver.Host}/account",
                    AssetBundleUrlList = GetAssetBundleUrlList(Version),
                    ExAudioAndVideoUrlList = GetExAudioAndVideoUrlList(Version),
                    ExResourceUrlList = GetExResourceUrlList(Version),
                    Ext = GetExt(Version),
                    Gameserver = Gameserver,
                    Gateway = Gameserver,
                    IsDataReady = true,
                    OaserverUrl = $"http://{Global.config.Gameserver.Host}/oaserver",
                    ServerCurTime = Global.GetUnixInSeconds(),
                    ServerCurTimezone = 8,
                    ServerExt = new ServerExt()
                    {
                        CdkeyUrl = $"http://{Global.config.Gameserver.Host}/common",
                        MihoyoSdkEnv = "2"
                    }
                };
                ctx.Response.Headers.Add("Content-Type", "application/json");
                return ctx.Response.WriteAsync(JsonConvert.SerializeObject(rsp));
            });
        }

        public static Ext GetExt(string version)
        {
            return new Ext()
            {
                AiUseAssetBoundle = "1",
                ApmLogLevel = "2",
                ApmSwitch = "1",
                ApmSwitchCrash = "1",
                DataUseAssetBoundle = "1",
                EnableWatermark = "1",
                ExAudioAndVideoUrlList = GetExAudioAndVideoUrlList(version),
                ExResPrePublish = "0",
                ExResUseHttp = "0",
                ExResourceUrlList = GetExResourceUrlList(version),
                ForbidRecharge = "1",
                IsChecksumOff = "1",
                OfflineReportSwitch = "1",
                ResUseAssetBoundle = "1",
                ShowVersionText = "0",
                UpdateStreamingAsb = "1",
                UseMultyCdn = "1",
                ApmLogDest = "2",
                ApmSwitchGameLog = "1",
                BlockErrorDialog = "1",
                ElevatorModelPath = "GameEntry/EVA/StartLoading_Model",
                ExResBuffSize = "10485760",
                IsXxxx = "0",
                MtpSwitch = "0",
                NetworkFeedbackEnable = "0",
                ShowBulletinButton = "0",
                ShowBulletinEmptyDialogBg = "0"
            };
        }

        public static string[] GetAssetBundleUrlList(string version)
        {
            Regex regex = new Regex(@"^(.*?)_(os|gf|global)_(.*?)$");
            Match matches = regex.Match(version);

            if (matches.Success)
            {
                string type = matches.Groups[2].Value; // get the second group (os or gf)

                switch (type)
                {
                    case "os":
                        return Global.config.UseLocalCache ? new string[]
                        {
                            $"http://{Global.config.Gameserver.Host}/asset_bundle/overseas01/1.1",
                            $"http://{Global.config.Gameserver.Host}/asset_bundle/overseas01/1.1"
                        } : new string[]
                        {
                            "https://hk-bundle-os-mihayo.akamaized.net/asset_bundle/overseas01/1.1",
                            "https://bundle-aliyun-os.honkaiimpact3.com/asset_bundle/overseas01/1.1"
                        };
                    case "gf":
                        if (version.Contains("beta"))
                        {
                            return Global.config.UseLocalCache ? new string[]
                            {
                                $"https://{Global.config.Gameserver.Host}/asset_bundle/beta_release/1.0",
                                $"https://{Global.config.Gameserver.Host}/asset_bundle/beta_release/1.0"
                            } : new string[]
                            {
                                "https://bh3rd-beta-qcloud.bh3.com/asset_bundle/beta_release/1.0",
                                "https://bh3rd-beta.bh3.com/asset_bundle/beta_release/1.0"
                            };
                        }
                        return Global.config.UseLocalCache ? new string[]
                        {
                            $"https://{Global.config.Gameserver.Host}/asset_bundle/android01/1.0",
                            $"https://{Global.config.Gameserver.Host}/asset_bundle/android01/1.0"
                        } : new string[]
                        {
                            "https://bundle-qcloud.bh3.com/asset_bundle/android01/1.0",
                            "https://bundle.bh3.com/asset_bundle/android01/1.0"
                        };
                    case "global":
                        return Global.config.UseLocalCache ? new string[]
                        {
                            $"https://{Global.config.Gameserver.Host}/asset_bundle/usa01/1.1",
                            $"https://{Global.config.Gameserver.Host}/asset_bundle/usa01/1.1"
                        } : new string[]
                        {
                            "http://hk-bundle-west-mihayo.akamaized.net/asset_bundle/usa01/1.1",
                            "http://bundle-aliyun-usa.honkaiimpact3.com/asset_bundle/usa01/1.1"
                        };
                    default:
                        return Global.config.UseLocalCache ? new string[]
                        {
                            $"http://{Global.config.Gameserver.Host}/asset_bundle/overseas01/1.1",
                            $"http://{Global.config.Gameserver.Host}/asset_bundle/overseas01/1.1"
                        } : new string[]
                        {
                            "https://hk-bundle-os-mihayo.akamaized.net/asset_bundle/overseas01/1.1",
                            "https://bundle-aliyun-os.honkaiimpact3.com/asset_bundle/overseas01/1.1"
                        };
                }
            }
            else
            {
                return Global.config.UseLocalCache ? new string[]
                {
                    $"http://{Global.config.Gameserver.Host}/asset_bundle/overseas01/1.1",
                    $"http://{Global.config.Gameserver.Host}/asset_bundle/overseas01/1.1"
                } : new string[]
                {
                    "https://hk-bundle-os-mihayo.akamaized.net/asset_bundle/overseas01/1.1",
                    "https://bundle-aliyun-os.honkaiimpact3.com/asset_bundle/overseas01/1.1"
                };
            }
        }
        
        public static string[] GetExAudioAndVideoUrlList(string version)
        {
            Regex regex = new(@"^(.*?)_(os|gf|global)_(.*?)$");
            Match matches = regex.Match(version);

            if (matches.Success)
            {
                string type = matches.Groups[2].Value; // get the second group (os or gf)

                switch (type)
                {
                    case "os":
                        return Global.config.UseLocalCache ? new string[]
                        {
                            $"{Global.config.Gameserver.Host}/com.miHoYo.bh3oversea",
                            $"{Global.config.Gameserver.Host}/com.miHoYo.bh3oversea"
                        } : new string[]
                        {
                            "hk-bigfile-os-mihayo.akamaized.net/com.miHoYo.bh3oversea",
                            "bigfile-aliyun-os.honkaiimpact3.com/com.miHoYo.bh3oversea"
                        };
                    case "gf":
                        if (version.Contains("beta"))
                        {
                            return Global.config.UseLocalCache ? new string[]
                            {
                                $"{Global.config.Gameserver.Host}/tmp/CGAudio",
                                $"{Global.config.Gameserver.Host}/tmp/CGAudio"
                            } : new string[]
                            {
                                "bh3rd-beta-qcloud.bh3.com/tmp/CGAudio",
                                "bh3rd-beta.bh3.com/tmp/CGAudio"
                            };
                        }
                        return new string[] { };
                    case "global":
                        return new string[] { };
                    default:
                        return new string[] { };
                }
            }
            else
            {
                return new string[] { };
            }
        }
        
        public static string[] GetExResourceUrlList(string version)
        {
            Regex regex = new(@"^(.*?)_(os|gf|global)_(.*?)$");
            Match matches = regex.Match(version);

            if (matches.Success)
            {
                string type = matches.Groups[2].Value; // get the second group (os or gf)

                switch (type)
                {
                    case "os":
                        return Global.config.UseLocalCache ? new string[]
                        {
                            $"{Global.config.Gameserver.Host}/com.miHoYo.bh3oversea",
                            $"{Global.config.Gameserver.Host}/com.miHoYo.bh3oversea"
                        } : new string[]
                        {
                            "hk-bigfile-os-mihayo.akamaized.net/com.miHoYo.bh3oversea",
                            "bigfile-aliyun-os.honkaiimpact3.com/com.miHoYo.bh3oversea"
                        };
                    case "gf":
                        if (version.Contains("beta"))
                        {
                            return Global.config.UseLocalCache ? new string[]
                            {
                                $"{Global.config.Gameserver.Host}/tmp/beta",
                                $"{Global.config.Gameserver.Host}/tmp/beta"
                            } : new string[]
                            {
                                "bh3rd-beta-qcloud.bh3.com/tmp/beta",
                                "bh3rd-beta.bh3.com/tmp/beta"
                            };
                        }
                        return Global.config.UseLocalCache ? new string[]
                        {
                            $"{Global.config.Gameserver.Host}/tmp/Original",
                            $"{Global.config.Gameserver.Host}/tmp/Original"
                        } : new string[]
                        {
                            "bundle-qcloud.bh3.com/tmp/Original",
                            "bundle.bh3.com/tmp/Original"
                        };
                    case "global":
                        return Global.config.UseLocalCache ? new string[]
                        {
                            $"{Global.config.Gameserver.Host}/tmp/com.miHoYo.bh3global",
                            $"{Global.config.Gameserver.Host}/tmp/com.miHoYo.bh3global"
                        } : new string[]
                        {
                            "hk-bundle-west-mihayo.akamaized.net/tmp/com.miHoYo.bh3global",
                            "bigfile-aliyun-usa.honkaiimpact3.com/tmp/com.miHoYo.bh3global"
                        };
                    default:
                        return Global.config.UseLocalCache ? new string[]
                        {
                            $"{Global.config.Gameserver.Host}/com.miHoYo.bh3oversea",
                            $"{Global.config.Gameserver.Host}/com.miHoYo.bh3oversea"
                        } : new string[]
                        {
                            "hk-bigfile-os-mihayo.akamaized.net/com.miHoYo.bh3oversea",
                            "bigfile-aliyun-os.honkaiimpact3.com/com.miHoYo.bh3oversea"
                        };
                }
            }
            else
            {
                return Global.config.UseLocalCache ? new string[]
                {
                    $"{Global.config.Gameserver.Host}/com.miHoYo.bh3oversea",
                    $"{Global.config.Gameserver.Host}/com.miHoYo.bh3oversea"
                } : new string[]
                {
                    "hk-bigfile-os-mihayo.akamaized.net/com.miHoYo.bh3oversea",
                    "bigfile-aliyun-os.honkaiimpact3.com/com.miHoYo.bh3oversea"
                };
            }
        }
    }
}
