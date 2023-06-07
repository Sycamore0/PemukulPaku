using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.SetWarshipReq)]
    internal class SetWarshipReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            SetWarshipReq Data = packet.GetDecodedBody<SetWarshipReq>();

            session.Player.User.WarshipId = (int)Data.WarshipId;

            GetMainDataRsp MainDataRsp = new()
            {
                retcode = GetMainDataRsp.Retcode.Succ,
                WarshipTheme = new() { WarshipId = (uint)session.Player.User.WarshipId },
                TypeLists = new uint[] { 38 }
            };
            SetWarshipRsp Rsp = new() { retcode = SetWarshipRsp.Retcode.Succ };

            session.Send(Packet.FromProto(MainDataRsp, CmdId.GetMainDataRsp), Packet.FromProto(Rsp, CmdId.SetWarshipRsp));
        }
    }
}
