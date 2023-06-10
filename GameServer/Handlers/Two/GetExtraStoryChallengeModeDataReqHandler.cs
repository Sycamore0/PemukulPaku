using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Two
{
    [PacketCmdId(CmdId.GetExtraStoryChallengeModeDataReq)]
    internal class GetExtraStoryChallengeModeDataReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetExtraStoryChallengeModeDataReq Data = packet.GetDecodedBody<GetExtraStoryChallengeModeDataReq>();

            session.Send(Packet.FromProto(new GetExtraStoryChallengeModeDataRsp()
            {
                retcode = GetExtraStoryChallengeModeDataRsp.Retcode.Succ,
                ChooseDifficulty = 0,
                IsCanReset = true,
                ChapterId = Data.ChapterId
            }, CmdId.GetExtraStoryChallengeModeDataRsp));
        }
    }
}
