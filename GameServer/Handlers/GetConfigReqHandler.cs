using Common;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetConfigReq)]
    internal class GetConfigReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetConfigRsp Rsp = new()
            {
                retcode = GetConfigRsp.Retcode.Succ,
                ServerCurTime = (uint)Global.GetUnixInSeconds(),
            };

            session.Send(Packet.FromProto(Rsp, CmdId.GetConfigRsp));
        }
    }
}
