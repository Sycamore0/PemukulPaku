using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetRpgTaleReq)]
    internal class GetRpgTaleReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetRpgTaleReq Data = packet.GetDecodedBody<GetRpgTaleReq>();

            session.Send(Packet.FromProto(new GetRpgTaleRsp() { retcode = GetRpgTaleRsp.Retcode.Succ, IsAll = Data.IsAll, TaleId = Data.TaleId }, CmdId.GetRpgTaleRsp));
        }
    }
}
