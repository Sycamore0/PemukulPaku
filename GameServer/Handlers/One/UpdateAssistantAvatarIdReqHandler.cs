using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.UpdateAssistantAvatarIdReq)]
    public class UpdateAssistantAvatarIdReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            UpdateAssistantAvatarIdReq Data = packet.GetDecodedBody<UpdateAssistantAvatarIdReq>();
            session.Player.User.AssistantAvatarId = (int)Data.AvatarId;

            UpdateAssistantAvatarIdRsp Rsp = new() { retcode = UpdateAssistantAvatarIdRsp.Retcode.Succ };
            GetMainDataRsp MainDataRsp = new() { retcode = GetMainDataRsp.Retcode.Succ, AssistantAvatarId = (uint)session.Player.User.AssistantAvatarId, TypeLists = new uint[] { 19 } };

            session.Send(Packet.FromProto(MainDataRsp, CmdId.GetMainDataRsp), Packet.FromProto(Rsp, CmdId.UpdateAssistantAvatarIdRsp));
        }
    }
}
