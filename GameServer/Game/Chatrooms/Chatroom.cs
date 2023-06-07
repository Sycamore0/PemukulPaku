using Common;
using Common.Database;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Commands;

namespace PemukulPaku.GameServer.Game.Chatrooms
{
    public class Chatroom
    {
        public readonly uint Id;
        public readonly List<Session> Members = new();
        public readonly List<ChatMsg> Messages = new();

        public Chatroom(uint id)
        {
            Id = id;
        }

        public void Join(in Session session)
        {
            Members.Add(session);
        }

        public void OnSendChat(in Session session, ChatMsg chatMsg)
        {
            string? StringMsg = chatMsg.Content.Items.Where(item => item.MsgStr != null).FirstOrDefault()?.MsgStr;

            if (StringMsg != null)
            {
                // do we need cmds?
                List<string> args = StringMsg.Split(' ').ToList();
                Command? Cmd = CommandFactory.Commands.Find(cmd => args[0] == cmd.Name.ToLower());
                if (Cmd != null)
                {
                    args.RemoveAt(0);

                    try
                    {
                        if (Cmd.CmdType == CommandType.All || Cmd.CmdType == CommandType.Player)
                        {
                            Cmd.Run(session, args.ToArray());
                            if (Cmd.CmdType == CommandType.Player)
                                SendAiMsg("Command executed", session);
                        }
                        else
                        {
                            SendAiMsg("Invalid usage", session);
                        }
                    }
                    catch (Exception ex)
                    {
                        SendAiMsg(ex.Message, session);
                    }
                    return;
                }

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

            Broadcast(chatMsg);
            Messages.Add(chatMsg);
        }

        public void Broadcast(ChatMsg chatMsg)
        {
            foreach (Session session in Members)
            {
                RecvChatMsgNotify notify = new() { };
                notify.ChatMsgLists.Add(chatMsg);

                session.Send(Packet.FromProto(notify, CmdId.RecvChatMsgNotify));
            }
        }

        public void SendAiMsg(string msg, Session? session)
        {
            if(session == null)
            {
                foreach (Session valSession in Members)
                {
                    SendAiMsg(msg, valSession);
                }
                return;
            }
                

            RecvChatMsgNotify notify = new() { };
            ChatMsg AiMsg = new()
            {
                Uid = 0,
                Nickname = "Ai-chan",
                Time = (uint)Global.GetUnixInSeconds(),
                Msg = msg,
                Content = new() { },
                Channel = ChatMsg.MsgChannel.World,
                AvatarId = 3201,
                DressId = 593201,
                FrameId = 200001,
                CustomHeadId = 161080,
                CheckResult = new() { NumberCheck = 0, ShieldType = 0, RewriteText = msg }
            };
            AiMsg.Content.Items.Add(new() { MsgStr = msg });

            notify.ChatMsgLists.Add(AiMsg);

            session.Send(Packet.FromProto(notify, CmdId.RecvChatMsgNotify));
        }
    }

    public abstract class BaseChatroom<TSelf> where TSelf : BaseChatroom<TSelf>
    { 
        public readonly Dictionary<uint, Chatroom> Chatrooms = new();
        private static TSelf? Instance;

        public static TSelf GetInstance()
        {
            return Instance ??= Activator.CreateInstance<TSelf>();
        }

        public virtual Chatroom Join(in Session session, uint Id)
        {
            Chatrooms.TryGetValue(Id, out Chatroom? chatroom);
            if(chatroom != null)
            { 
                chatroom.Join(session);
            }
            else
            {
                Chatrooms.Add(Id, new Chatroom(Id));
                return Join(session, Id);
            }
            return chatroom;
        }

        public virtual void Left(Session session)
        {
            Chatroom? chatroom = Chatrooms.Values.Where(chatr => chatr.Members.Contains(session)).FirstOrDefault();
            if (chatroom != null)
            {
                chatroom.Members.Remove(session);
                if (chatroom.Members.Count < 1)
                    Chatrooms.Remove(chatroom.Id);
            }
        }

        public virtual Chatroom GetChatroom(Session session)
        {
            Chatroom? chatroom = Chatrooms.Values.Where(chatr => chatr.Members.Contains(session)).FirstOrDefault();
            return chatroom ?? Join(session, 1);
        }
    }
}
