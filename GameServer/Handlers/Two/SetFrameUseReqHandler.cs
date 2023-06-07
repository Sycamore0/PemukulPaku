using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Two
{
    [PacketCmdId(CmdId.SetFrameUseReq)]
    internal class SetFrameUseReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            SetFrameUseReq Data = packet.GetDecodedBody<SetFrameUseReq>();
            session.Player.User.FrameId = (int)Data.FrameId;
            GetMainDataRsp mainDataRsp = new()
            {
                retcode = GetMainDataRsp.Retcode.Succ,
                UseFrameId = Data.FrameId,
                TypeLists = new uint[] { 26 }
            };

            session.Send(Packet.FromProto(mainDataRsp, CmdId.GetMainDataRsp), Packet.FromProto(new SetFrameUseRsp() { FrameId = Data.FrameId, retcode = SetFrameUseRsp.Retcode.Succ }, CmdId.SetFrameUseRsp));
        }
    }
}
