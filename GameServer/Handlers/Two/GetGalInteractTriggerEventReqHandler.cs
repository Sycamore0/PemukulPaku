using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetGalInteractTriggerEventReq)]
    internal class GetGalInteractTriggerEventReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetGalInteractTriggerEventReq Data = packet.GetDecodedBody<GetGalInteractTriggerEventReq>();

            Random random = new ();
            GetGalInteractTriggerEventRsp Rsp = new()
            {
                retcode = GetGalInteractTriggerEventRsp.Retcode.Succ,
                AvatarId = Data.AvatarId,
                EventId = Data.EventIdLists[random.Next(0, Data.EventIdLists.Length - 1)]
            };

            session.Send(Packet.FromProto(Rsp, CmdId.GetGalInteractTriggerEventRsp));
        }
    }
}
