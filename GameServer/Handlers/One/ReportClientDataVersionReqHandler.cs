using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.One
{
    [PacketCmdId(CmdId.ReportClientDataVersionReq)]
    internal class ReportClientDataVersionReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            ReportClientDataVersionReq Data = packet.GetDecodedBody<ReportClientDataVersionReq>();

            session.Send(Packet.FromProto(new ReportClientDataVersionRsp() { ServerVersion = Data.Version }, CmdId.ReportClientDataVersionRsp));
        }
    }
}
