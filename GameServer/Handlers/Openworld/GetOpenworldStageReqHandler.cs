using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Openworld
{
    [PacketCmdId(CmdId.GetOpenworldStageReq)]
    internal class GetOpenworldStageReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetOpenworldStageReq Data = packet.GetDecodedBody<GetOpenworldStageReq>();

            session.Send(Packet.FromProto(new GetOpenworldStageRsp()
            {
                retcode = GetOpenworldStageRsp.Retcode.Succ,
                MapId = Data.MapId,
                MechaLostHpPercent = 0,
                MechaLostSpPercent = 0,
                MapEnergy = 0,
                ScDlcFeverScore = 0,
                ScDlcClimaxScore = 0
            }, CmdId.GetOpenworldStageRsp));
        }
    }
}
