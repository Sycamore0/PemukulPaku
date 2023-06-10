using Common.Resources.Proto;
using Common;
using System.Text;

namespace PemukulPaku.GameServer.Commands
{

    [CommandHandler("help", " - shows help page >:3", CommandType.All)]
    internal class HelpCommand : Command
    {
        public override void Run(Session session, string[] args)
        {
            RecvChatMsgNotify notify = new();
            //hardcoding values is fun AND easy!
            StringBuilder msg = new("<color=#B00B><size=26>Commands</size></color><size=16><color=#555>\n");
            msg.Append("command <required> [optional]\n");
            //msg.Append("┌\n");
            foreach (Command Cmd in CommandFactory.Commands)
            {
                if (Cmd.CmdType == CommandType.All || Cmd.CmdType == CommandType.Player)
                {
                    msg.Append("┝<size=22>" + Cmd.Name + " " + Cmd.Description + "</size>\n");
                    if (Cmd.Examples is not null)
                    {
                        foreach (string Example in Cmd.Examples)
                        {
                            msg.Append("│┕" + Example + "\n");
                        }
                    }
                }
            }
            msg.Append("</color></size>");

            //I really want to figure out how to grab from Chatroom.cs instead, but oh well.
            ChatMsg AiMsg = new()
            {
                Uid = 0,
                Nickname = "Ai-chan",
                Time = (uint)Global.GetUnixInSeconds(),
                Msg = msg.ToString(),
                Content = new(),
                Channel = ChatMsg.MsgChannel.World,
                AvatarId = 3201,
                DressId = 593201,
                FrameId = 200001,
                CustomHeadId = 161080,
                CheckResult = new() { NumberCheck = 0, ShieldType = 0, RewriteText = msg.ToString() }
            };
            AiMsg.Content.Items.Add(new() { MsgStr = msg.ToString() });
            notify.ChatMsgLists.Add(AiMsg);
            session.Send(Packet.FromProto(notify, CmdId.RecvChatMsgNotify));
        }

        public override void Run(string[] args)
        {
            foreach (Command Cmd in CommandFactory.Commands)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("      " + Cmd.Name);
                Console.ResetColor();
                c.Trail(Cmd.Description);
            }
        }
    }
}
