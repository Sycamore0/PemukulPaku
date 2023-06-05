using Common;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetGodWarReq)]
    internal class GetGodWarReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetGodWarRsp Rsp = new() { retcode = GetGodWarRsp.Retcode.Succ };

            GodWar godWar = new()
            {
                GodWarId = 1,
                BeginTime = 0,
                EndTime = (uint)Global.GetUnixInSeconds() * 12 / 10,
                MaxSupportPoint = 95,
                LobbyId = 1,
                CurChapterId = 1,
                RoleInfo = new()
                {

                }
            };
            godWar.ChapterLists.AddRange(new uint[] { 1, 2, 3 }.Select(x => new GodWarChapter() { ChapterId = x }));

            session.Send(Packet.FromProto(Rsp, CmdId.GetGodWarRsp));
        }
    }
}
