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
                Player? player = Session.FromUid(user.Uid)?.Player;
                player ??= new(user);

                Rsp.CardData = player.GetCardData();
                Rsp.PlayerData = player.GetDetailData();
            }
            else
            {
                Rsp.retcode = GetOtherPlayerCardDataRsp.Retcode.Fail;
            }

            session.Send(Packet.FromProto(Rsp, CmdId.GetOtherPlayerCardDataRsp));
        }
    }
}
