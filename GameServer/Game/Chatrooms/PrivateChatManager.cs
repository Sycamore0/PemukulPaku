using Common.Database;
using Common;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Game.Chatrooms
{
    public static class PrivateChatManager
    {
        public static void OnSendChatMsg(Session session, SendChatMsgNotify chatMsgNotify)
        {
            ChatMsg chatMsg = chatMsgNotify.ChatMsg;
            string? StringMsg = chatMsg.Content.Items.Where(item => item.MsgStr != null).FirstOrDefault()?.MsgStr;
            if (StringMsg != null)
            {
                chatMsg.CheckResult = new()
                {
                    ShieldType = 0,
                    NumberCheck = 0,
                    RewriteText = StringMsg
                };
                chatMsg.Msg = StringMsg;
            }
            UserScheme User = session.Player.User;
            chatMsg.Uid = User.Uid;
            chatMsg.Nickname = User.Nick;
            chatMsg.Time = (uint)Global.GetUnixInSeconds();
            chatMsg.AvatarId = (uint)User.AssistantAvatarId;
            chatMsg.DressId = session.Player.AvatarList.Where(avatar => avatar.AvatarId == User.AssistantAvatarId).First().DressId;
            chatMsg.FrameId = User.FrameId < 200001 ? 200001 : (uint)User.FrameId;
            chatMsg.CustomHeadId = (uint)User.CustomHeadId;

            foreach (uint targetUid in chatMsgNotify.TargetUidLists)
            {
                SendPrivateMessage(session, targetUid, chatMsg);
            }
        }

        public static void SendPrivateMessage(Session session, uint targetUid, ChatMsg chatMsg)
        {
            PrivateMessageScheme privateMessage = PrivateMessage.Create(session.Player.User.Uid, targetUid, chatMsg);
            Session? targetSession = Session.FromUid(targetUid);
            RecvChatMsgNotify notify = new() { };
            notify.ChatMsgLists.Add(privateMessage.Msg);

            session.Send(Packet.FromProto(notify, CmdId.RecvChatMsgNotify));
            targetSession?.Send(Packet.FromProto(notify, CmdId.RecvChatMsgNotify));
        }
    }
}
