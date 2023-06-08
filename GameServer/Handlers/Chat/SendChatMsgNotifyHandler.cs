using Common.Database;
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
            if (Data.ChatMsg.Channel == ChatMsg.MsgChannel.World)
                WorldChatroom.GetInstance().GetChatroom(session).OnSendChat(session, Data.ChatMsg);
            else if (Data.ChatMsg.Channel == ChatMsg.MsgChannel.Private)
                PrivateChatManager.OnSendChatMsg(session, Data);
        }
    }
}
