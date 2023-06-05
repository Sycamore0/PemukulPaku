using Common.Resources.Proto;
using PemukulPaku.GameServer.Game.Chatrooms;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.EnterWorldChatroomReq)]
    internal class EnterWorldChatroomReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            EnterWorldChatroomReq Data = packet.GetDecodedBody<EnterWorldChatroomReq>();
            WorldChatroom.GetInstance().Left(session);
            Chatroom JoinedChatroom = WorldChatroom.GetInstance().Join(session, Data.ChatroomId == 0 ? 1 : Data.ChatroomId);

            EnterWorldChatroomRsp Rsp = new()
            {
                retcode = EnterWorldChatroomRsp.Retcode.Succ,
                ChatroomId = JoinedChatroom.Id,
                ActivityType = ActivityWorldChatroomType.ActivityWorldChatroomTypeNone,
                PlayerNum = (uint)JoinedChatroom.Members.Count
            };
            Rsp.HisChatMsgLists.AddRange(JoinedChatroom.Messages.Skip(JoinedChatroom.Messages.Count - 5).ToList());

            session.Send(Packet.FromProto(Rsp, CmdId.EnterWorldChatroomRsp));
        }
    }
}
