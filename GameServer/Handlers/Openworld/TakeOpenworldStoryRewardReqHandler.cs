using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Openworld
{
    [PacketCmdId(CmdId.TakeOpenworldStoryRewardReq)]
    internal class TakeOpenworldStoryRewardReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            TakeOpenworldStoryRewardReq Data = packet.GetDecodedBody<TakeOpenworldStoryRewardReq>();
            UserScheme.OpenWorldStoryScheme? ow = session.Player.User.OpenWorldStory.FirstOrDefault(x => x.StoryId == Data.StoryId);
            if (ow is not null)
                ow.IsDone = true;

            session.Send(Packet.FromProto(new TakeOpenworldStoryRewardRsp() { retcode = TakeOpenworldStoryRewardRsp.Retcode.Succ, StoryId = Data.StoryId }, CmdId.TakeOpenworldStoryRewardRsp));
            session.ProcessPacket(Packet.FromProto(new GetOpenworldStoryReq() { }, CmdId.GetOpenworldStoryReq));
        }
    }
}
