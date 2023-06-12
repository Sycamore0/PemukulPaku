using Common.Resources.Proto;
using Common.Database;

namespace PemukulPaku.GameServer.Handlers.Openworld
{
    [PacketCmdId(CmdId.SetOpenworldStoryProgressReq)]
    internal class SetOpenworldStoryProgressReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            SetOpenworldStoryProgressReq Data = packet.GetDecodedBody<SetOpenworldStoryProgressReq>();
            UserScheme.OpenWorldStoryScheme? ow = session.Player.User.OpenWorldStory.FirstOrDefault(x => x.StoryId == Data.StoryId);
            if (ow is not null)
                ow.StoryProgress = Data.StoryProgress;

            session.Send(Packet.FromProto(new SetOpenworldStoryProgressRsp() { retcode = SetOpenworldStoryProgressRsp.Retcode.Succ, StoryId = Data.StoryId }, CmdId.SetOpenworldStoryProgressRsp));
            session.ProcessPacket(Packet.FromProto(new GetOpenworldStoryReq() { }, CmdId.GetOpenworldStoryReq));
        }
    }
}
