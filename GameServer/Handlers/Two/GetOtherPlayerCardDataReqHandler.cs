using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers.Two
{
    [PacketCmdId(CmdId.GetOtherPlayerCardDataReq)]
    internal class GetOtherPlayerCardDataReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetOtherPlayerCardDataReq Data = packet.GetDecodedBody<GetOtherPlayerCardDataReq>();
            UserScheme? user = User.FromUid(Data.TargetUid);
            GetOtherPlayerCardDataRsp Rsp = new() { retcode = GetOtherPlayerCardDataRsp.Retcode.Succ, TargetUid = Data.TargetUid };

            if(user is not null)
            {
                Player player = new(user);
                Rsp.CardData = new()
                {
                    Uid = player.User.Uid,
                    MsgData = new()
                    {
                        MsgIndex = 0,
                        MsgConfig = 1
                    },
                    OnPhonePendantId = 350005
                };
                Rsp.PlayerData = new()
                {
                    Uid = player.User.Uid,
                    Nickname = player.User.Nick,
                    Level = (uint)PlayerLevelData.GetInstance().CalculateLevel(player.User.Exp).Level,
                    SelfDesc = player.User.SelfDesc,
                    CustomHeadId = (uint)player.User.CustomHeadId,
                    FrameId = player.User.FrameId < 200001 ? 200001 : (uint)player.User.FrameId,
                    LeaderAvatar = player.AvatarList.FirstOrDefault(x => x.AvatarId == player.User.AvatarTeamList.FirstOrDefault()?.AvatarIdLists[0])?.ToDetailData(player.Equipment) ?? new() { AvatarId = 101 }
                };
            }
            else
            {
                Rsp.retcode = GetOtherPlayerCardDataRsp.Retcode.Fail;
            }

            session.Send(Packet.FromProto(Rsp, CmdId.GetOtherPlayerCardDataRsp));
        }
    }
}
