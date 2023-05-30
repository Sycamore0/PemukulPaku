using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetAuthkeyReq)]
    internal class GetAuthkeyReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetAuthkeyReq Data = packet.GetDecodedBody<GetAuthkeyReq>();

            GetAuthkeyRsp Rsp = new()
            {
                retcode = GetAuthkeyRsp.Retcode.Succ,
                AuthAppid = Data.AuthAppid,
                Authkey = "0",
                SignType = 2,
                AuthkeyVer = 1
            };

            session.Send(Packet.FromProto(Rsp, CmdId.GetAuthkeyRsp));
        }
    }
}
