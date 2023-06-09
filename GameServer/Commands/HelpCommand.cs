using Common.Resources.Proto;
using Common;

namespace PemukulPaku.GameServer.Commands
{

    [CommandHandler("help", " - shows help page >:3", CommandType.All)]
    internal class HelpCommand : Command
    {
        public override void Run(Session session, string[] args)
        {
            RecvChatMsgNotify notify = new() { };
            //hardcoding values is fun AND easy!
            string msg = "<color=#B00B><size=26>Commands</size></color><size=16><color=#555>\n";
            msg += "command <required> [optional]\n";
            //msg += "┌\n";
            foreach (Command Cmd in CommandFactory.Commands)
            {
                if(Cmd.CmdType == CommandType.All || Cmd.CmdType == CommandType.Player)
                {
                    msg += "┝<size=22>" + Cmd.Name + " " + Cmd.Description + "</size>\n";
                    if (Cmd.Examples is not null)
                    {
                        foreach (string Example in Cmd.Examples)
                        {
                            msg += "│┕" + Example + "\n";
                        }
                    }
                }                
            }
            msg += "</color></size>";

            //I really want to figure out how to grab from Chatroom.cs instead, but oh well.
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

        public override void Run(string[] args)
        {
            foreach (Command Cmd in CommandFactory.Commands)
            {
                Console.ForegroundColor= ConsoleColor.White;
                Console.WriteLine("      " + Cmd.Name);
                Console.ResetColor();
                c.Trail(Cmd.Description);
            }
        }
    }
}
