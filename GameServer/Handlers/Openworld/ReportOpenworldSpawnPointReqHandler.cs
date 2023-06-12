using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Openworld
{
    [PacketCmdId(CmdId.ReportOpenworldSpawnPointReq)]
    internal class ReportOpenworldSpawnPointReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            ReportOpenworldSpawnPointReq Data = packet.GetDecodedBody<ReportOpenworldSpawnPointReq>();
            ReportOpenworldSpawnPointRsp Rsp = new()
            {
                retcode = ReportOpenworldSpawnPointRsp.Retcode.Succ,
                MapId = Data.MapId,
                PointInfo = Data.PointInfo
            };
            OpenWorldScheme? OpenWorldData = session.Player.OpenWorlds.FirstOrDefault(x => x.MapId == Data.MapId);
            if (OpenWorldData is not null)
                OpenWorldData.SpawnPoint = Data.PointInfo;

            session.Send(Packet.FromProto(Rsp, CmdId.ReportOpenworldSpawnPointRsp));
        }
    }
}
