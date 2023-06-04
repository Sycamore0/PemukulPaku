using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetClientDataReq)]
    internal class GetClientDataReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetClientDataReq Data = packet.GetDecodedBody<GetClientDataReq>();

            session.Send(Packet.FromProto(new GetClientDataRsp() { retcode = GetClientDataRsp.Retcode.NotFound, Type = Data.Type, Id = Data.Id }, CmdId.GetClientDataRsp));
        }
    }
}
