using Common;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetGachaDisplayReq)]
    internal class GetGachaDisplayReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetGachaDisplayRsp Rsp = new()
            {
                retcode = GetGachaDisplayRsp.Retcode.Succ,
                IsAll = true,
                Type = GachaType.GachaTypeError,
                GachaRandom = (uint)Global.GetUnixInSeconds()
            };
            Rsp.GachaDisplayInfoLists.Add(new()
            {
                GachaType = GachaType.GachaCustomAvatar,
                CommonData = new()
                {
                    TitleImage = "SpriteOutput/Gacha/TitleKakin3",
                    Title = "5.18~6.16",
                    Content = "To feed ur gacha addiction",
                    IsEnablePrompt = true,
                    GachaId = 30115600,
                    DataBeginTime = 1684353600,
                    DataEndTime = 2684353600,
                    UpAvatarLists = AvatarData.GetInstance().All.Where(avatar => avatar.AvatarId < 9000).Select(avatar => (uint)avatar.AvatarId).ToArray()
                },
                CustomGachaData = new()
                {
                    TicketHcoinCost = 280,
                    TicketMaterialId = 1103,
                    IsEnableBaodi = true,
                    GachaType = GachaType.GachaCustomAvatar,
                    GachaTimes = 0,
                    DisplayMaxTimes = 100,
                    NoProtectGachaTimes = 0,
                    DisplayVideoAvatar = 0,
                    ShiningType = 1,
                    ExId = 1,
                    ProtectDisplayInfo = new()
                    {
                        NoProtectGachaTimes = 0,
                        DisplayKeyAvatar = 0,
                        protect_display_type = GachaProtectDisplayInfo.ProtectDisplayType.NoDisplay
                    }
                }
            });

            session.Send(Packet.FromProto(Rsp, CmdId.GetGachaDisplayRsp));
        }
    }
}
