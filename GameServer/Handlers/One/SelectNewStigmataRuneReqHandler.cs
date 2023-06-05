using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.SelectNewStigmataRuneReq)]
    internal class SelectNewStigmataRuneReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            SelectNewStigmataRuneReq Data = packet.GetDecodedBody<SelectNewStigmataRuneReq>();
            SelectNewStigmataRuneRsp Rsp = new() { retcode = SelectNewStigmataRuneRsp.Retcode.Succ };

            Packet.c.Log($"SelectNewStigmataRuneReqHandler: {Data.UniqueId} {Data.SelectUniqueId} {Data.IsSelect}");

            if (Data.IsSelect)
            {
            }

            session.Send(Packet.FromProto(Rsp, CmdId.SelectNewStigmataRuneRsp));
        }
    }
}
