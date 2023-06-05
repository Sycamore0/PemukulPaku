using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetStageRecommendAvatarReq)]
    internal class GetStageRecommendAvatarReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetStageRecommendAvatarReq Data = packet.GetDecodedBody<GetStageRecommendAvatarReq>();
            GetStageRecommendAvatarRsp Rsp = new() { retcode = GetStageRecommendAvatarRsp.Retcode.Succ };
            if(Data.IdLists is not null)
            {
                Rsp.StageRecommendAvatarLists.AddRange(Data.IdLists.Select(x => new StageRecommendAvatar() { Id = x }));
            }

            session.Send(Packet.FromProto(Rsp, CmdId.GetStageRecommendAvatarRsp));
        }
    }
}
