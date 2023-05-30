using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetCustomHeadDataReq)]
    internal class GetCustomHeadDataReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetCustomHeadDataRsp Rsp = new() { retcode = GetCustomHeadDataRsp.Retcode.Succ, IsAll = true };
            Rsp.CustomHeadLists.Add(new CustomHead() { Id = 161001 });

            session.Send(Packet.FromProto(Rsp, CmdId.GetCustomHeadDataRsp));
        }
    }
}