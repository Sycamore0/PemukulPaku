using Common.Resources.Proto;
using PemukulPaku.GameServer.Game.Chatrooms;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.SendChatMsgNotify)]
    internal class SendChatMsgNotifyHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            SendChatMsgNotify Data = packet.GetDecodedBody<SendChatMsgNotify>();
            WorldChatroom.GetInstance().GetChatroom(session).OnSendChat(session, Data.ChatMsg);
        }
    }
}
