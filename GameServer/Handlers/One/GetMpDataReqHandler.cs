using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.One
{
    [PacketCmdId(CmdId.GetMpDataReq)]
    internal class GetMpDataReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetMpDataRsp Rsp = new()
            {
                retcode = GetMpDataRsp.Retcode.Succ,
                DataType = MpDataType.MpDataAll,
                op_type = GetMpDataRsp.OpType.InitData,
                MpLevel = 0,
                MpExp = 0,
                TeamAvatarId = session.Player.GetDetailData().LeaderAvatar.AvatarId,
                PunishEndTime = 0
            };

            session.Send(Packet.FromProto(Rsp, CmdId.GetMpDataRsp));
        }
    }
}
