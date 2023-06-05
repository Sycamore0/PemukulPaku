using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetFrameDataReq)]
    internal class GetFrameDataReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetFrameDataRsp Rsp = new() { retcode = GetFrameDataRsp.Retcode.Succ, IsAll = true };
            Rsp.FrameLists.Add(new FrameData() { Id = 200001 });

            session.Send(Packet.FromProto(Rsp, CmdId.GetFrameDataRsp));
        }
    }
}
