using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Three
{
    [PacketCmdId(CmdId.SetCustomHeadReq)]
    internal class SetCustomHeadReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            SetCustomHeadReq Data = packet.GetDecodedBody<SetCustomHeadReq>();
            session.Player.User.CustomHeadId = (int)Data.Id;

            GetMainDataRsp mainDataRsp = new()
            {
                retcode = GetMainDataRsp.Retcode.Succ,
                CustomHeadId = Data.Id,
                TypeLists = new uint[] { 36 }
            };

            session.Send(Packet.FromProto(mainDataRsp, CmdId.GetMainDataRsp), Packet.FromProto(new SetCustomHeadRsp() { retcode = SetCustomHeadRsp.Retcode.Succ }, CmdId.SetCustomHeadRsp));
        }
    }
}
