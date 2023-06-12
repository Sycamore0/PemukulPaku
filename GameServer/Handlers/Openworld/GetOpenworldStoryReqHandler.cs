using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Openworld
{
    [PacketCmdId(CmdId.GetOpenworldStoryReq)]
    internal class GetOpenworldStoryReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetOpenworldStoryRsp Rsp = new()
            {
                retcode = GetOpenworldStoryRsp.Retcode.Succ,
                FinishStoryIdLists = session.Player.User.OpenWorldStory.Where(x => x.IsDone).Select(x => x.StoryId).ToArray(),
                IsAll = true
            };
            Rsp.CurStoryLists.AddRange(session.Player.User.OpenWorldStory.Where(x => !x.IsDone).Select(x => x.ToProto()));

            session.Send(Packet.FromProto(Rsp, CmdId.GetOpenworldStoryRsp));
        }
    }
}
