using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Openworld
{
    [PacketCmdId(CmdId.AcceptOpenworldStoryReq)]
    internal class AcceptOpenworldStoryReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            AcceptOpenworldStoryReq Data = packet.GetDecodedBody<AcceptOpenworldStoryReq>();
            AcceptOpenworldStoryRsp Rsp = new()
            {
                retcode = AcceptOpenworldStoryRsp.Retcode.Succ,
                StoryId = Data.StoryId
            };
            session.Player.User.AddOWStory(Data.StoryId);

            session.Send(Packet.FromProto(Rsp, CmdId.AcceptOpenworldStoryRsp));
            session.ProcessPacket(Packet.FromProto(new GetOpenworldStoryReq() { }, CmdId.GetOpenworldStoryReq));
        }
    }
}
