using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetStageChapterReq)]
    internal class GetStageChapterReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetStageChapterRsp Rsp = new() { retcode = GetStageChapterRsp.Retcode.Succ };

            for (uint i = 1; i < 38; i++)
            {
                Rsp.ChapterLists.Add(new() { ChapterId = i, HasTakeChallenge = 0 });
            }

            session.Send(Packet.FromProto(Rsp, CmdId.GetStageChapterRsp));
        }
    }
}
