using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.SetSelfDescReq)]
    internal class SetSelfDescReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            SetSelfDescReq Data = packet.GetDecodedBody<SetSelfDescReq>();
            session.Player.User.SelfDesc = Data.SelfDesc;
            GetMainDataRsp mainDataRsp = new()
            {
                retcode = GetMainDataRsp.Retcode.Succ,
                SelfDesc = Data.SelfDesc,
                TypeLists = new uint[] { 16 }
            };
            SetSelfDescRsp Rsp = new() { retcode = SetSelfDescRsp.Retcode.Succ };

            session.Send(Packet.FromProto(mainDataRsp, CmdId.GetMainDataRsp), Packet.FromProto(Rsp, CmdId.SetSelfDescRsp));
        }
    }
}
