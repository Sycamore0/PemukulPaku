using Common;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.One
{
    [PacketCmdId(CmdId.GetWeekDayActivityDataReq)]
    internal class GetWeekDayActivityDataReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetWeekDayActivityDataRsp Rsp = new() { retcode = GetWeekDayActivityDataRsp.Retcode.Succ };

            Rsp.ActivityLists.Add(new()
            {
                ActivityId = 1003,
                StageIdLists = new uint[] { 101302, 101303, 101304, 101305 },
                EnterTimes = 1,
                BeginTime = 0,
                EndTime = (uint)Global.GetUnixInSeconds() + 3600 * 24 * 7,
                ActivityEndTime = (uint)Global.GetUnixInSeconds() * (10 / 8),
                ForceOpenTime = 0
            });

            session.Send(Packet.FromProto(Rsp, CmdId.GetWeekDayActivityDataRsp));
        }
    }
}
