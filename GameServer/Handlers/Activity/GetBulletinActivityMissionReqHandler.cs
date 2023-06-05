using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetBulletinActivityMissionReq)]
    internal class GetBulletinActivityMissionReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetBulletinActivityMissionReq Data = packet.GetDecodedBody<GetBulletinActivityMissionReq>();

            GetBulletinActivityMissionRsp Rsp = new() { retcode = GetBulletinActivityMissionRsp.Retcode.Succ };
            Rsp.MissionGroupLists.AddRange(Data.ActivityIdLists.Select(activityId => new BulletinMissionGroup() { ActivityId = activityId }).ToList());

            session.Send(Packet.FromProto(Rsp, CmdId.GetBulletinActivityMissionRsp));
        }
    }
}
