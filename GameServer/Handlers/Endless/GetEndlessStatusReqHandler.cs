using Common;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetEndlessStatusReq)]
    internal class GetEndlessStatusReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetEndlessStatusRsp Rsp = new()
            {
                retcode = GetEndlessStatusRsp.Retcode.Succ,
                CurStatus = new()
                {
                    BeginTime = 0,
                    EndTime = (uint)Global.GetUnixInSeconds() + 3600 * 24 * 7,
                    CloseTime = (uint)Global.GetUnixInSeconds() + 3600 * 24 * 7 + 1200,
                    EndlessType = EndlessType.EndlessTypeUltra,
                    CanJoinIn = true
                },
                SelectedEndlessType = 5
            };

            session.Send(Packet.FromProto(Rsp, CmdId.GetEndlessStatusRsp));
        }
    }
}
