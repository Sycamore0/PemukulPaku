using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Chat
{
    [PacketCmdId(CmdId.GetPrivateHistoryChatMsgReq)]
    internal class GetPrivateHistoryChatMsgReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetPrivateHistoryChatMsgReq Data = packet.GetDecodedBody<GetPrivateHistoryChatMsgReq>();
            GetPrivateHistoryChatMsgRsp Rsp = new() { retcode = GetPrivateHistoryChatMsgRsp.Retcode.Succ };

            if (Data.UidLists.Length > 0)
            {
                foreach (uint targetUid in Data.UidLists)
                {
                    Rsp.ChatLists.AddRange(PrivateMessage.GetMessages(session.Player.User.Uid, targetUid));
                }
            }
            else
            {
                Rsp.ChatLists.AddRange(PrivateMessage.GetMessages(session.Player.User.Uid));
            }

            session.Send(Packet.FromProto(Rsp, CmdId.GetPrivateHistoryChatMsgRsp));
        }
    }
}
