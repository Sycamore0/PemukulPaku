using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetFrameDataReq)]
    internal class GetFrameDataReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetFrameDataRsp Rsp = new() { retcode = GetFrameDataRsp.Retcode.Succ, IsAll = true };
            Rsp.FrameLists.AddRange(Common.Utils.ExcelReader.FrameData.GetInstance().All.Select(x => new FrameData() { Id = (uint)x.Id }));

            session.Send(Packet.FromProto(Rsp, CmdId.GetFrameDataRsp));
        }
    }
}
