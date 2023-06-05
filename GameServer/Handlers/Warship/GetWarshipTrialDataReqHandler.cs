using Common;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetWarshipTrialDataReq)]
    internal class GetWarshipTrialDataReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetWarshipTrialDataRsp Rsp = new()
            {
                retcode = GetWarshipTrialDataRsp.Retcode.Succ,
                IsAll = true
            };
            Rsp.TrialWarshipLists.Add(new TrialWarship()
            {
                SampleId = 410002,
                EndTime = (uint)Global.GetUnixInSeconds() + 400000
            });

            session.Send(Packet.FromProto(Rsp, CmdId.GetWarshipTrialDataRsp));
        }
    }
}
