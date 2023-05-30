using Common;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.ClientReportReq)]
    internal class ClientReportReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            ClientReportReq Data = packet.GetDecodedBody<ClientReportReq>();

            if((int)Global.config.VerboseLevel > 0) 
                session.c.Warn($"ClientReport | {Data.ReportType} =  {Data.ReportValue}");

            session.Send(Packet.FromProto(new ClientReportRsp() { retcode = ClientReportRsp.Retcode.Succ }, CmdId.ClientReportRsp));
        }
    }
}
