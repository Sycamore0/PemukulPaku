using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.ChapterBwWorldGetDataReq)]
    internal class ChapterBwWorldGetDataReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            ChapterBwWorldGetDataReq Data = packet.GetDecodedBody<ChapterBwWorldGetDataReq>();

            session.Send(Packet.FromProto(new ChapterBwWorldGetDataRsp()
            {
                retcode = ChapterBwWorldGetDataRsp.Retcode.Succ,
                ChapterBwWorld = new()
                {
                    ChapterId = Data.ChapterId,
                    EquipRuneUniqueIdLists = new uint[] { 0, 0 }
                }
            }, CmdId.ChapterBwWorldGetDataRsp));
        }
    }
}
