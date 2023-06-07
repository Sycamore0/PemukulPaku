using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Three
{
    [PacketCmdId(CmdId.GetOtherPlayerClientSettingReq)]
    internal class GetOtherPlayerClientSettingReqHadler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetOtherPlayerClientSettingReq Data = packet.GetDecodedBody<GetOtherPlayerClientSettingReq>();
            GetOtherPlayerClientSettingRsp Rsp = new()
            {
                retcode = GetOtherPlayerClientSettingRsp.Retcode.Succ,
                ClientSettingType = Data.ClientSettingType,
                TargetUid = Data.TargetUid,
                IsWeeklyGuideSwitchOn = false
            };

            session.Send(Packet.FromProto(Rsp, CmdId.GetOtherPlayerClientSettingRsp));
        }
    }
}
